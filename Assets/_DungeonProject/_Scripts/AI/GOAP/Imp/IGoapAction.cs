
using UnityEngine;
using System.Collections.Generic;

public interface IGoapAction {
	IEnumerable<KeyValuePair<string, object>> GetPreconditions();
	IEnumerable<KeyValuePair<string, object>> GetEffects();

	GameObject Target { get; set; }
	float Cost { get; set; }

	void reset();
	bool checkProceduralPrecondition(GameObject agent);
	IEnumerator<PerformState> perform(GameObject agent);
	bool isInRange();
}