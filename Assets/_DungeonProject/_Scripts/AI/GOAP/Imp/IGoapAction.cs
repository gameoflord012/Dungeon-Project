
using UnityEngine;
using System.Collections.Generic;

public interface IGoapAction {
	IEnumerable<KeyValuePair<string, object>> GetPreconditions();
	IEnumerable<KeyValuePair<string, object>> GetEffects();

	float Cost { get; set; }

	void reset();
	bool checkProceduralPrecondition(GoapAgent agent);
	Vector3 GetTargetPosition();
	IEnumerator<PerformState> perform(GoapAgent agent);
	bool isInRange();
}