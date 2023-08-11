using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WithinRange : Conditional
{
	public bool useMax = true;
	public float max;
	public bool useMin = true;
	public float min;
	public SharedFloat value;	
	public override TaskStatus OnUpdate()
	{
		if(!useMax)
		{
			if(min <= value.Value)
			{
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}
		}
		else if(!useMin)
		{
			if(value.Value <= max)
			{
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}
		}
		else
		{
			if(min <= value.Value && value.Value <= max)
			{
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Failure;
			}
			
		}
	}
}