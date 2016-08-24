// Authored by Sarang Bhadsavle on April 25th, 2016

using UnityEngine;
using System.Collections;

/*
# BBlackbody color datafile (bbr_color.txt)
# Mitchell Charity 
# http://www.vendian.org/mncharity/dir3/blackbody/
---http://www.vendian.org/mncharity/dir3/blackbody/UnstableURLs/bbr_color.html
# Version 2001-Jun-22
# History:
#   2001-Jun-22  Switched to sRGB from Rec.709.
#   2001-Jun-04  Initial release.
# Fields:
#  K     temperature (K)
#  CMF   {" 2deg","10deg"}
#          either CIE 1931  2 degree CMFs with Judd Vos corrections
#              or CIE 1964 10 degree CMFs
#  x y   chromaticity coordinates
#  P     power in semi-arbitrary units
#  R G B {0-1}, normalized, mapped to gamut, logrithmic
#        (sRGB primaries and gamma correction)
#  r g b {0-255}
#  #rgb  {00-ff} 
#
*/

/*
F.lux color temperature values with
Mitchell Charity's black blody colour datafile (credited below)

What are the PC presets in Kelvin?
Ember:             1200K ---> 255  83   0
Candle:            1900K ---> 255 131   0
Warm Incandescent: 2300K ---> 255 152  54
Incandescent:      2700K ---> 255 169  87
Halogen:           3400K ---> 255 193 132
Fluorescent:       4200K ---> 255 213 173
Daylight:          5500K ---> 255 236 224
*/

public class LightBehaviourScript : MonoBehaviour {

	private bool enable = true;

	private Color daylight;
	private Color fluorescent;
	private Color halogen;
	private Color incandescent;
	private Color warmIncandescent;
	private Color candle;
	private Color ember;

	//private Color currentColor;

	private SunBehaviourScript sunb;
	private float dayLength;
	private float dayCount = 0;

	//private float lerpVal = 0.0;

	// Use this for initialization
	void Start () {
		daylight = new Color (255, 236, 224);
		fluorescent = new Color (255, 213, 173);
		halogen = new Color (255, 193, 132);
		incandescent = new Color (255, 169,  87);
		warmIncandescent = new Color (255, 152, 54);
		candle = new Color (255, 131, 0);
		ember = new Color (255, 83, 0);

		sunb = GameObject.Find ("Sun").GetComponent<SunBehaviourScript> ();
		dayLength = sunb.getDayLengthCount (); // a value in # of frames (e.g. 900)

		this.gameObject.GetComponent<Light> ().color = daylight;
	}
	
	// Update is called once per frame
	void Update () {
		dayCount += 1;
		if (enable) {
			//this.gameObject.GetComponent<Light> ().color = Color.Lerp(daylight, ember, Time.time/*Mathf.PingPong(Time.time * 0.5f, 1.0f)*/);
			//print (this.gameObject.GetComponent<Light> ().color);
			if (dayCount < dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (daylight, fluorescent, dayCount / (dayLength / 12));
			} else if (dayCount < 2 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (fluorescent, halogen, (dayCount - 1 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 3 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (halogen, incandescent, (dayCount - 2 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 4 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (incandescent, warmIncandescent, (dayCount - 3 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 5 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (warmIncandescent, candle, (dayCount - 4 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 6 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (candle, ember, (dayCount - 5 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 7 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (ember, candle, (dayCount - 6 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 8 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (candle, warmIncandescent, (dayCount - 7 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 9 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (warmIncandescent, incandescent, (dayCount - 8 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 10 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (incandescent, halogen, (dayCount - 9 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 11 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (halogen, fluorescent, (dayCount - 10 * (dayLength / 12)) / (dayLength / 12));
			} else if (dayCount < 12 * dayLength / 12) {
				this.gameObject.GetComponent<Light> ().color = Color.Lerp (fluorescent, daylight, (dayCount - 11 * (dayLength / 12)) / (dayLength / 12));
			} else {
				dayCount = 0;
			}
			//print (this.gameObject.GetComponent<Light> ().color);
			//print(getLongFromColor(new Color(100,104,98)));
			//long el = getLongFromColor(new Color(100,104,98));
			//long el = getLongFromColor(this.gameObject.GetComponent<Light> ().color);
			//print(getColorFromLong(el));
			//print (this.gameObject.GetComponent<Light> ().color);
		} 
		else {
			this.gameObject.GetComponent<Light> ().color = daylight;
		}
	}

	public long getLongFromColor(Color col){
		long red = (long) Mathf.Round (col.r);
		long green = (long) Mathf.Round (col.g) << 8;
		long blue = (long) Mathf.Round (col.b) << 16;

		return red + blue + green;
	}

	public Color getColorFromLong(long value){
		byte blue = (byte) value;
		value = value >> 8;
		byte green = (byte) value;
		value = value >> 8;
		byte red = (byte) value;
		return new Color (red, green, blue);
	}

	public long getCurrentLightConfiguration(){
		return getLongFromColor(this.gameObject.GetComponent<Light> ().color);
	}

	public float getCurrentLightConfiguration_Blue(){
		return this.gameObject.GetComponent<Light> ().color.b;
	}

	public float getCurrentLightConfiguration_Green(){
		return this.gameObject.GetComponent<Light> ().color.g;
	}

	public float getCurrentLightConfiguration_Red(){
		return this.gameObject.GetComponent<Light> ().color.r;
	}
}
