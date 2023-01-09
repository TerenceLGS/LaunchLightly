using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using LaunchLightly.Models.LaunchDarkly;
using LaunchLightly.Services;
using ReactiveUI;

namespace LaunchLightly.ViewModels;

public class MainWindowViewModel : ViewModelBase {
	private readonly ILaunchDarklyApi _api;

	public MainWindowViewModel(ILaunchDarklyApi launchDarklyApi) {
		_api = launchDarklyApi;
		GetCurrentFromLd = ReactiveCommand.CreateFromTask(GetCurrentStateFromLd);
		AddValuesToClauseCommand = ReactiveCommand.CreateFromTask(AddValuesToClause);
		RemoveValuesFromClauseCommand = ReactiveCommand.CreateFromTask(RemoveValuesFromClause);
		ReplaceValuesInClauseCommand = ReactiveCommand.CreateFromTask(ReplaceValuesInClause);
	}

	public ReactiveCommand<Unit, Unit> GetCurrentFromLd { get; }

	public async Task GetCurrentStateFromLd() {
		string result;
		if (GetFlag) {
			result = await _api.GetFlag(GetKeys(), CancellationToken.None);
		} else { result = await _api.GetSegment(GetKeys(), CancellationToken.None); }

		ResultsJson = result;
	}
	
	public ReactiveCommand<Unit, Unit> AddValuesToClauseCommand { get; }
	
	private LdApiKeys GetKeys() => new LdApiKeys(ApiKey, SdkKey, ProjectId, EnvironmentKey, FlagId);

	
	public async Task AddValuesToClause() {
		string result = null;
		if (GetSegment) {
			var values = PayloadIdsPerLine.Select(v => new KeyValuePair<string, int>(v, 0));

			result = await _api.UpdateSegmentClause(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, "add", values, ChangeComment);
		} else {
			var values = PayloadIdsPerLine;
			result = await _api.UpdateFlagClauseWithSemantic(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, add, values, ChangeComment);
		}

		
		ResultsJson = result;
	}
	
	public ReactiveCommand<Unit, Unit> RemoveValuesFromClauseCommand { get; }

	public async Task RemoveValuesFromClause() {
		string? result = null;
		if (_getSegment) {
			var valuesWithIndexes = _api.FindIndexesOf(LastSegment, IntRuleIndex, IntClauseIndex, PayloadIdsPerLine);
			result = await _api.UpdateSegmentClause(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, "remove", valuesWithIndexes, ChangeComment);
		} else {
			result = await _api.UpdateFlagClauseWithSemantic(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, remove, PayloadIdsPerLine, ChangeComment);
		}
		ResultsJson = result;
	}
	
	public ReactiveCommand<Unit, Unit> ReplaceValuesInClauseCommand { get; }

	public async Task ReplaceValuesInClause() {
		string? result = null;
		if (GetSegment) {
			var values = PayloadIdsPerLine.Select(v => new KeyValuePair<string, int>(v, 0));
			var clauseSize = LastSegment.rules[IntRuleIndex].clauses[IntClauseIndex].values.Count;
			result = await _api.UpdateSegmentClause(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, "replace", values, ChangeComment, clauseSize);
		} else {
			result = await _api.UpdateFlagClauseWithSemantic(GetKeys(), CancellationToken.None, RuleIndex, ClauseIndex, replace, PayloadIdsPerLine, ChangeComment);
		}

		ResultsJson = result;
	}

	private bool? _revealKeys = true;
	private string _sdkKey = String.Empty;
	private string _apiKey = String.Empty;
	private string _projectId = String.Empty;
	private string _environmentKey = String.Empty;
	private string _flagId = String.Empty;
	private string _resultsJson = string.Empty;
	private string _ruleIndex = string.Empty;
	private string _clauseIndex = string.Empty;
	private string _payloadIds = string.Empty;
	private string _changeComment = string.Empty;
	private string _messages = string.Empty;
	private bool _getFlag = true;
	private bool _getSegment = false;
	private Segment? _lastSegment;
	private Flag? _lastFlag;
	private JsonSerializerOptions JsonOptions { get; set; } = new JsonSerializerOptions(){DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault, WriteIndented = true};

	private Segment? LastSegment {
		get {
			if (ResultsJson.Any()) { _lastSegment = JsonSerializer.Deserialize<Segment>(ResultsJson, JsonOptions); }
			return _lastSegment;
		}

		set {
			_lastSegment = value;
			this.RaisePropertyChanged();
		}
	}

	private Flag? LastFlag {
		get {
			if (ResultsJson.Any()) { _lastFlag = JsonSerializer.Deserialize<Flag>(ResultsJson, JsonOptions); }
			return _lastFlag;
		}
		set {
			_lastFlag = value;
			this.RaisePropertyChanged();
		}
	}

	public IEnumerable<string> PayloadIdsPerLine => PayloadIds.Split('\n', StringSplitOptions.TrimEntries);
	public string PayloadIds {
		get => _payloadIds;
		set {
			_payloadIds = value;
			this.RaisePropertyChanged();
		}
	}

	public string SdkKey {
		get => _sdkKey;
		set {
			_sdkKey = value;
			this.RaisePropertyChanged();
		}
	}

	public string ApiKey {
		get => _apiKey;
		set {
			_apiKey = value;
			this.RaisePropertyChanged();
		}
	}

	public string ProjectId {
		get => _projectId;
		set {
			_projectId = value;
			this.RaisePropertyChanged();
		}
	}

	public string EnvironmentKey {
		get => _environmentKey;
		set {
			_environmentKey = value;
			this.RaisePropertyChanged();
		}
	}

	public string FlagId {
		get => _flagId;
		set {
			_flagId = value;
			this.RaisePropertyChanged();
		}
	}

	public string ChangeComment {
		get => _changeComment;
		set {
			_changeComment = value;
			this.RaisePropertyChanged();
		}
	}

	private int IntRuleIndex => Convert.ToInt32(RuleIndex);
	public string RuleIndex {
		get => _ruleIndex;
		set {
			_ruleIndex = value;
			this.RaisePropertyChanged();
		}
	}

	public string Messages {
		get => _messages;
		set {
			_messages = value;
			this.RaisePropertyChanged();
		}
	}

	private int IntClauseIndex => Convert.ToInt32(ClauseIndex);
	public string ClauseIndex {
		get => _clauseIndex;
		set {
			_clauseIndex = value;
			this.RaisePropertyChanged();
		}
	}

	public bool? RevealKeys {
		get => _revealKeys;
		set {
			_revealKeys = value;
			this.RaisePropertyChanged();
		}
	}

	public string ResultsJson {
		get => _resultsJson;
		set {
			_resultsJson = value;
			this.RaisePropertyChanged();
		}
	}

	public bool GetFlag {
		get => _getFlag;
		set {
			_getFlag = value;
			this.RaisePropertyChanged();
		}
	}
	public bool GetSegment {
		get => _getSegment;
		set {
			_getSegment = value;
			this.RaisePropertyChanged();
		}
	}
}