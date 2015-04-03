namespace Sparkle.Engine.Base
{
	/// <summary>
	/// A trigger state.
	/// </summary>
	public enum Trigger
	{
		// The trigger is inactive.
		Inactive,
		// The trigger has just started being active.
		Started,
		// The trigger is inactive.
		Active,
		// The trigger has just started being inactive.
		Stopped,
	}
}
