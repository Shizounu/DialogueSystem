using UnityEngine;

namespace Dialogue.Data
{
    public class Information : DialogueElement
    {
        public Blackboard Blackboard;
        public ComparisonOperator Operator;
        public string FactKey;
        public int Value;
        public override bool CanEnter()
        {
            return true;
        }

        public override void OnEnter()
        {
            if (!Blackboard.Facts.ContainsKey(FactKey))
            {
                Debug.LogError($"Key ({FactKey}) is not present in Blackboard {Blackboard.name}");
                throw new System.Exception($"Key ({FactKey}) is not present in Blackboard {Blackboard.name}");
            }

            switch (Operator)
            {
                case ComparisonOperator.Addition:
                    Blackboard.Facts[FactKey] += Value;
                    break;
                case ComparisonOperator.Subtraction:
                    Blackboard.Facts[FactKey] -= Value;
                    break;
                case ComparisonOperator.Set:
                    Blackboard.Facts[FactKey] = Value;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }

    public enum ComparisonOperator
    {
        Addition,
        Subtraction,
        Set
    }
}
