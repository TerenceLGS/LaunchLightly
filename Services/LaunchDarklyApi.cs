using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LaunchLightly.Models;
using LaunchLightly.Models.LaunchDarkly;
using LaunchLightly.Models.LaunchDarkly.Semantic;

namespace LaunchLightly.Services; 

public class LaunchDarklyApi {
	public static readonly string SemanticPatchHeader = "application/json; domain-model=launchdarkly.semanticpatch";
	public static readonly string AcceptHeader = "application/json";
	public static readonly string BaseAddress = "https://app.launchdarkly.com/api/v2/";
	public static readonly string FlagsAddress = BaseAddress + "flags";
	public static readonly string SegmentAddress = BaseAddress + "segments";
	
	private HttpClient Http { get; set; }

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
							op = "oneof",
						},
						new Clause() {
							attribute = "MemberID",
							values = new List<string>() { "a", "b", "c", "d" },
							op = "oneof",
						},
					},
				}
			},
			key = "seg",
			version = 3,
			
		};
		return JsonSerializer.Serialize(segment);
		HttpRequestMessage message = new(HttpMethod.Get, $"{SegmentAddress}/{keys.Project}/{keys.Environment}/{keys.Key}");
		AddHeaders(message, keys.ApiKey, false);
		var response = await Http.SendAsync(message, cancellation);
		var result = await response.Content.ReadAsStringAsync(cancellation);
		return result;
	}

	private void AddHeaders(HttpRequestMessage request, string apiToken, bool semantic = false) {
		request.Headers.Add("LD-API-Version", "beta");
		request.Headers.Add("Authorization", apiToken);
		request.Headers.Add("Accept", AcceptHeader);
		if (semantic) { request.Headers.Add("Content-Type", SemanticPatchHeader); }
	}

	public async Task<string> UpdateSegmentClause(LdApiKeys keys, CancellationToken cancellation, string ruleId, string clauseId, string kind, IEnumerable<string> values, string? comment) {
		// https://apidocs.launchdarkly.com/tag/Segments#operation/patchSegment
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, $"{SegmentAddress}/{keys.Project}/{keys.Environment}/{keys.Key}");
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

		var response = await Http.SendAsync(request, cancellation);
		var result = await response.Content.ReadAsStringAsync(cancellation);
		return result;
	}
}