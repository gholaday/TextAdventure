using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextColors {

	public enum TextColor {

		White,
		Black,
		Orange,
		Aqua,
		Blue,
		Red, 
		Green,
		Yellow
	};

	public static Dictionary<TextColor, string> ColorStrings = new Dictionary<TextColor, string> ()
	{
		{TextColor.White, "<color=white>"},
		{TextColor.Black, "<color=black>"},
		{TextColor.Orange, "<color=orange>"},
		{TextColor.Aqua, "<color=aqua>"},
		{TextColor.Blue, "<color=blue>"},
		{TextColor.Red, "<color=red>"},
		{TextColor.Green, "<color=green>"},
		{TextColor.Yellow, "<color=yellow>"}

	};

	public static string GetTag(TextColor tColor)
	{
		return ColorStrings [tColor];
	}


}
