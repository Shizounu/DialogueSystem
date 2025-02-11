using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Dialogue.Data
{
    public class Conditional : DialogueElement {
        public Blackboard Blackboard;
        public string FactKey;
        public ConditionOperator Operator;
        public int Value;
        public override bool CanEnter()
        {
            if (!Blackboard.Facts.ContainsKey(FactKey))
            {
                Debug.LogError($"Key ({FactKey}) is not present in Blackboard {Blackboard.name}");
                throw new System.Exception($"Key ({FactKey}) is not present in Blackboard {Blackboard.name}");
            }

            return Operator switch
            {
                ConditionOperator.Greater => Blackboard.Facts[FactKey] > Value,
                ConditionOperator.GreaterOrEqual => Blackboard.Facts[FactKey] >= Value,
                ConditionOperator.Equal => Blackboard.Facts[FactKey] == Value,
                ConditionOperator.NotEqual => Blackboard.Facts[FactKey] != Value,
                ConditionOperator.LessOrEqual => Blackboard.Facts[FactKey] <= Value,
                ConditionOperator.Less => Blackboard.Facts[FactKey] < Value,
                _ => throw new System.NotImplementedException(),
            };
        }
    
        public override void OnEnter() { }
    }

    public enum ConditionOperator
    {
        Greater,
        GreaterOrEqual,
        Equal,
        NotEqual,
        LessOrEqual,
        Less
    }
}
