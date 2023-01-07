using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using LaunchLightly.Models;
using LaunchLightly.Models.LaunchDarkly;
using LaunchLightly.Models.LaunchDarkly.JsonPatch;
using LaunchLightly.Models.LaunchDarkly.Semantic;

namespace LaunchLightly.Services;


public class ClauseOp {
	public static string In = "in";
	public static string StartsWith = "startswith";
	public static string EndsWith = "endswith";
	
}

public interface ILaunchDarklyApi {
	Task<Rule> GetRule(LdApiKeys keys, CancellationToken cancellation);
	Task<string> GetSegment(LdApiKeys keys, CancellationToken cancellation);
	Task<string> UpdateFlagClauseWithSemantic(LdApiKeys keys, CancellationToken cancellation, string ruleId, string clauseId, string kind, IEnumerable<string> values, string? comment);
	IEnumerable<KeyValuePair<string, int>> FindIndexesOf(Segment segment, int ruleIndex, int clauseIndex, IEnumerable<string> values);
	Task<string> UpdateSegmentClause(LdApiKeys keys, CancellationToken cancellation, string ruleIndex, string clauseIndex, string kind, IEnumerable<KeyValuePair<string, int>> values, string? comment, int? clauseSize = 0);
}

public class LaunchDarklyApi : ILaunchDarklyApi {
	public static readonly string SemanticPatchHeader = "application/json; domain-model=launchdarkly.semanticpatch";
	public static readonly string AcceptHeader = "application/json";
	public static readonly string BaseAddress = "https://app.launchdarkly.com/api/v2/";
	public static readonly string FlagsAddress = BaseAddress + "flags";
	public static readonly string SegmentAddress = BaseAddress + "segments";

	public async Task<Rule> GetRule(LdApiKeys keys, CancellationToken cancellation) {
		return null;
	}

	public async Task<string> GetSegment(LdApiKeys keys, CancellationToken cancellation) {
		var segment = new Segment() {
			name = "seg",
			rules = new List<Rule>() {
				new Rule() {
					_id = Guid.NewGuid().ToString(),
					clauses = new List<Clause>() {
						new Clause() {
							attribute = "GroupID",
							values = new List<string>() { "one", "two", "three", "four" },
							op = ClauseOp.In,
						},
						new Clause() {
							attribute = "MemberID",
							values = new List<string>() { "a", "b", "c", "d" },
							op = ClauseOp.In,
						},
					},
				}
			},
			key = "seg",
			version = 3,
			
		};
		return JsonSerializer.Serialize(segment, new JsonSerializerOptions(){WriteIndented = true});
		HttpRequestMessage message = new(HttpMethod.Get, $"{SegmentAddress}/{keys.Project}/{keys.Environment}/{keys.Key}");
		AddHeaders(message, keys.ApiKey, false);
		using var http = new HttpClient();
		var response = await http.SendAsync(message, cancellation);
		var result = await response.Content.ReadAsStringAsync(cancellation);
		return result;
	}

	private void AddHeaders(HttpRequestMessage request, string apiToken, bool semantic = false) {
		request.Headers.Add("LD-API-Version", "beta");
		request.Headers.Add("Authorization", apiToken);
		request.Headers.Add("Accept", AcceptHeader);
		if (semantic) { request.Headers.Add("Content-Type", SemanticPatchHeader); }
	}

	public async Task<string> UpdateFlagClauseWithSemantic(LdApiKeys keys, CancellationToken cancellation, string ruleId, string clauseId, string kind, IEnumerable<string> values, string? comment) {
		// https://apidocs.launchdarkly.com/tag/Segments#operation/patchSegment
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, $"{FlagsAddress}/{keys.Project}/{keys.Environment}/{keys.Key}");
		var _ = keys?.ApiKey ?? throw new ArgumentNullException(nameof(keys.ApiKey), "Need ApiKey for HTTP request");
		AddHeaders(request, keys.ApiKey, true);

		var body = new SemanticPatch<string>() {
			comment = comment,
			instructions = new List<Instruction<string>> {
				new() {
					clauseId = clauseId,
					ruleId = ruleId,
					kind = kind,
					values = values.ToList()
				}
			}
		};
		request.Content = new StringContent(JsonSerializer.Serialize(body));
		using var http = new HttpClient();
		var response = await http.SendAsync(request, cancellation);
		var result = await response.Content.ReadAsStringAsync(cancellation);
		return result;
		
	}

	public async Task<string> UpdateSegmentClause(LdApiKeys keys, CancellationToken cancellation, string ruleIndex, string clauseIndex, string kind, IEnumerable<KeyValuePair<string, int>> values, string? comment, int? clauseSize) {
		// https://apidocs.launchdarkly.com/tag/Segments#operation/patchSegment
		HttpRequestMessage request = new (HttpMethod.Patch, $"{SegmentAddress}/{keys.Project}/{keys.Environment}/{keys.Key}");
		var apiKey = keys?.ApiKey ?? throw new ArgumentNullException(nameof(keys.ApiKey), "Need ApiKey for HTTP request");
		AddHeaders(request, apiKey, false);
		var body = new SegmentPayload();
		if (kind == "replace") {
			var _ = clauseSize ?? throw new ArgumentNullException(nameof(clauseSize), "Need to provide clauseSize when replacing");
			for (int i = clauseSize.Value; i >= 0; i--) {
				body.patch.Add(new Operation() {
					op = "remove", 
					path = $"{ruleIndex}/{clauseIndex}/{i}"
				});
			}
		}
		if (kind is "add" or "replace") {
			foreach (KeyValuePair<string,int> item in values) {
				body.patch.Add(new Operation() {
					op = "add",
					path = $"{ruleIndex}/{clauseIndex}/-",
					value = item.Key
				});
			}
		}
		if (kind == "remove") {
			foreach (var item in values.OrderByDescending(i => i.Value)) {
				body.patch.Add(new Operation() {
					op = "remove",
					path = $"{ruleIndex}/{clauseIndex}/{item.Value}",
				});
			}
		}
		
		request.Content = new StringContent(JsonSerializer.Serialize(body, new JsonSerializerOptions() {DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull} ));
		using var http = new HttpClient();
		var response = await http.SendAsync(request, cancellation);
		var result = await response.Content.ReadAsStringAsync(cancellation);
		return result;
	}

	public IEnumerable<KeyValuePair<string, int>> FindIndexesOf(Segment segment, int ruleIndex, int clauseIndex, IEnumerable<string> values) {
		var rule = segment.rules[ruleIndex];
		var clause = rule.clauses[clauseIndex];
		var hashSet = values.ToHashSet();
		List<KeyValuePair<string, int>> results = new();
		for (int i = 0; i < clause.values.Count; i++) {
			if (hashSet.Contains(clause.values[i])) {
				results.Add(new KeyValuePair<string, int>(clause.values[i], i));
			}
		}

		return results;
	}
}