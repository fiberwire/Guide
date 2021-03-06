﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class ExtensionMethods {

    //returns a List<int> that contains the range of ints from _from to _to, optionally with an interval, _by
    public static List<int> to(this int _from, int _to, int by = 1) {
        var range = new List<int>();
        for (int i = _from; i <= _to; i += by) {
            range.Add(i);
        }
        return range;
    }

    //Calculates the absolute value of the difference of two ints
    public static int absDiff(this int from, int to) {
        return Mathf.Abs(from - to);
    }

    //Calculates the absolute value of the difference of two floats
    public static float absDiff(this float from, float to) {
        return Mathf.Abs(from - to);
    }

    //perform an action x times, passing the index to the action
    public static void times(this int x, Action<int> closure) {
        for (int i = 0; i < x; i++) {
            closure(i);
        }
    }
}
