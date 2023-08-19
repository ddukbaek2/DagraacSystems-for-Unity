using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace DagraacSystems.Unity
{
	/// <summary>
	/// 유닛테스트 특성.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class UnitTestAttribute : Attribute
	{
		/// <summary>
		/// 생성됨.
		/// </summary>
		public UnitTestAttribute()
		{
		}

		/// <summary>
		/// AssemblyCSharp에서 단위 작업 특성을 가진 모든 함수들을 반환.
		/// </summary>
		public static List<MethodInfo> GetUnitTestMethods()
		{
			var methodInfos = new List<MethodInfo>();
			var assembly = ReflectionUtility.GetAssemblyCSharp();
			if (assembly != null)
			{
				foreach (var classType in assembly.GetTypes())
				{
					foreach (var methodInfo in classType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
					{
						foreach (var attribute in methodInfo.GetCustomAttributes())
						{
							if (attribute is UnitTestAttribute)
							{
								methodInfos.Add(methodInfo);
								break;
							}
						}
					}
				}
			}

			return methodInfos;
		}
	}
}