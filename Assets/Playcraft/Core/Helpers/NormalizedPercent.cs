using UnityEngine;

namespace Playcraft
{
	public class NormalizedPercent
	{
		public float[] values;
		
		int length => values != null ? values.Length : 0; 

  		public void Normalize(float[] original)
		{
			CheckValuesInitialized(original);
		
	  		original = NormalMath.FixedNormal(values, original);

	  		for (int i = 0; i < length; i++)
	  			values[i] = original[i];
		}
		
		void CheckValuesInitialized(float[] original)
		{
			if (values == null || values.Length != original.Length)
			{
				values = new float[original.Length];
				
				for (int i = 0; i < original.Length; i++)
					values[i] = original[i];
			}
		}
		
		public int GetRandomIndex() { return GetIndexByThreshold(Random.Range(0f, 1f)); }

		public int GetIndexByThreshold(float threshold)
		{
			float accumulator = 0f;

			for (int i = 0; i < length; i++)
			{
				accumulator += values[i];

				if (accumulator >= threshold)
					return i;
			}

			Debug.LogError("GetIndexByThreshold failed: accumulator (" 
			+ accumulator + ") did not reach threshold (" + threshold + ")");
			return 0;
		}
	}

	public static class NormalMath
	{
		public static int GetChangedIndex(float[] array1, float[] array2)
		{
			int changedIndex = -1;

			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					changedIndex = i;
					break;
				}
			}

			return changedIndex;
		}

		// Normalize a distribution with an unknown value fixed
		public static float[] FixedNormal(float[] prior, float[] original)
		{
			return FixedNormal(original, GetChangedIndex(prior, original));
		}

		// Normalize a distribution with a known value fixed
		public static float[] FixedNormal(float[] original, int changedIndex)
		{
			int length = original.Length;
			float[] result = new float[length];

			if (changedIndex == -1)
				return original;

			for (int i = 0; i < length; i++)
				if (float.IsNaN(original[i]))
					original[i] = .333f;

			if (original[changedIndex] <= 0)
				original[changedIndex] = 0;
			else if (original[changedIndex] >= 1)
			{
				for (int i = 0; i < length; i++)
				{
					if (i == changedIndex)
						result[i] = 1;
					else
						result[i] = 0;
				}

				return result;
			}

			float sum = 0f;
			float leftover = 0f;
			for (int i = 0; i < length; i++)
			{
				if (i == changedIndex)
					leftover = 1 - original[i];
				else
					sum += original[i];
			}

			float[] percent = new float[length];
			for (int i = 0; i < length; i++)
			{
				if (i == changedIndex)
					result[i] = original[i];
				else
				{
					percent[i] = sum == 0 ? 0.5f : original[i]/sum;
					result[i] = leftover * percent[i];
				}
			}

			return result;
		}

		public static int NormalizedIndex(int array1Length, int array2Length, int array1Index)
		{
			if (array1Length == array2Length)
				return array1Index;
			if (array2Length == 1 || array1Index == 0)
				return 0;
			if (array1Length <= 0 || array2Length <= 0 || array1Index < 0)
			{
				Debug.LogError("Invalid arguments");
				return 0;
			}

			var normal = (array1Index + 1f)/(array1Length + 1f);
		    var array2Index = Mathf.FloorToInt(normal * array2Length);

		    if (array2Index >= array2Length)
		    {
				Debug.LogWarning("Calculated value out of range, adjusting down.  i1: " + array1Index + ", L1: " + array1Length + ", i2: " + array2Index + ", L2: " + array2Length);
				array2Index = array2Length - 1;
			}
			return array2Index;
		}
		
		public static float[] GetNormalizedArray(float[] array1, float[] array2, float percent)
		{
			array1 = GetNormalizedArray(array1);
			array2 = GetNormalizedArray(array2);
			
			var length = Mathf.Min(array1.Length, array2.Length);
			var result = new float[length];
			
			for (int i = 0; i < length; i++)
				result[i] = Mathf.Lerp(array1[i], array2[i], percent);
				
			return result;
		}
		
		public static float[] GetNormalizedArray(float[] original)
		{		
			var length = original.Length;
			if (length == 0) 
			{
				Debug.LogError("Cannot normalize array with zero elements");
				return original;
			}
			
			var result = new float[length];
			var sum = 0f;
			
			foreach (var item in original)
				sum += item;
				
			if (sum == 0f)
			{
				Debug.LogError("Cannot normalize array when sum of all values equals zero");
				return original;
			}
			
			for (int i = 0; i < length; i++)
				result[i] = original[i]/sum;
			
			return result;
		}
		
		public static int GetIndexFromProbabilities(float[] array, bool normalize = false)
		{
			if (normalize) array = GetNormalizedArray(array);
			
			float accumulator = 0f;
			float chance = Random.Range(0f, 1f);
			
			for (int i = 0; i < array.Length; i++)
			{
				accumulator += array[i];
				if (chance <= accumulator)
					return i;
			}
			
			return array.Length - 1;
		}
	}
}