using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FullAlert : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [SerializeField] private IdentifyTarget identifyTarget;

   public override TaskStatus OnUpdate()
   {
      targetObject.Value = GameObject.Find("Player");
      identifyTarget.alert = identifyTarget.maxAlert;
      return TaskStatus.Success;
   }
}