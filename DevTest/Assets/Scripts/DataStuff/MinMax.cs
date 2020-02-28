using System;

[Serializable]
public class MinMax <T> {
	public T min;
	public T max;
}
[Serializable] public class MinMaxFloat : MinMax<float> { }