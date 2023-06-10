using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    //物体淡入淡出时间间隔
    public const float itemFadeDuration = 0.35f;
    //物体淡出后的透明度
    public const float itemFadeAlpha = 0.5f;

    //时间系统相关
    public const float secondThreshold = 0.1f;  //数据越小时间越快

    public const int secondHold = 59;

    public const int minuteHold = 59;

    public const int hourHold = 23;

    public const int dayHold = 10;

    public const int seasonHold = 3;

}

