using System;
using Weaver.Heroes.Body;

namespace Weaver.Heroes.Mind;

/// <summary>
/// A skill condition to fullfill for managing the skill test.
/// </summary>
public interface ITestCondition {
	/// <summary>
	/// The skill difficulty threshold.
	/// </summary>
	int Difficulty { get; set; }
}

/// <summary>
/// The result of the skill.
/// </summary>
public interface ITestResult {
	/// <summary>
	/// True if the result is a success.
	/// <br/>
	/// Some RPG mechanics use more subtlties, 
	/// but should indicate if is at least an overall success.
	/// </summary>
	bool IsSucces { get; }
}

public interface ISkillTest {
	/// <summary>
	/// Do the skill test.
	/// </summary>
	/// <param name="conditions">The conditions of the test.</param>
	/// <returns></returns>
	ITestResult DoSkillTest( ITestCondition conditions );
}

public abstract class SkillModule : Module, ISkillTest
{
    public SkillModule(string moduleName) : base(moduleName)
    {
    }

    public abstract ITestResult DoSkillTest(ITestCondition conditions);
}