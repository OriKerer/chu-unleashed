using AOT;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;


namespace MarksAssets.FullscreenWebGL {
	public class FullscreenWebGL : MonoBehaviour
	{
		/*
			Makes your app go gullscreen. Possible options are "auto", "hide", "show". See https://developer.mozilla.org/en-US/docs/Web/API/FullscreenOptions/navigationUI
			You can also pass an empty string, in which case it defaults to "hide".
			If you called the "DetectFullscreen" method, then "EnterFullscreen" will also trigger "_onEnterFullscreenCallback"
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_EnterFullscreen")]
		public static extern void EnterFullscreen(string option);
		/*
			Makes your app exit fullscreen.
			If you called the "DetectFullscreen" method, then "ExitFullscreen" will also trigger "_onExitFullscreenCallback"
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_ExitFullscreen")]
		public static extern void ExitFullscreen();
		/*
			Switches between fullscreen and non-fullscreen
			If you called the "DetectFullscreen" method, then "Toggle" will trigger "_onEnterFullscreenCallback" when it switches to fullscreen,
			and call "_onExitFullscreenCallback" when it switches to non-fullscreen.
			
			You must call this method on a pointerdown event. It won't work on a pointerenter, pointerup, onclick, etc. It MUST be a pointerdown. See the example scene.
			If you want to know more about this, read https://forum.unity.com/threads/popup-blocker-and-pointerdown-pointerclick.383233/
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_Toggle")]
		public static extern void Toggle(string option);
		
		/*
			Attempts to detect fullscreen. If it's found, the method _onFullscreenDetectedCallback is called. If it's not, _onFullscreenNotDetectedCallback is called.
			If it is detected, it also calls _onEnterFullscreenCallback when the app enters fullscreen and calls _onExitFullscreenCallback when it exits fullscreen.
			If you want your app to react to fullscreen changes, you must call this method.
			
			If all you want to do is when the user clicks on start, the game starts and it goes fullscreen, but there is no actual mechanic that relies on fullscreen changes(for example, a fullscreen toggle button),
			then you don't need to call this method, because nothing in your app reacts to fullscreen changes.
		*/
		
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_Detect")]
		public static extern void DetectFullscreen();
		/*
			_onEnterFullscreenCallback and _onExitFullscreenCallback will no longer be called. Your app will no longer react to fullscreen changes.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_DetectStop")]
		public static extern void DetectStop();
		/*
			return true if your app is currently on fullscreen, false if not.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_IsFullscreen")]
		public static extern bool isFullscreen();
		/*
			return true if your app is capable of going fullscreen, false if not.
		*/
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_IsFullscreenDetected")]
		public static extern bool isFullscreenDetected();
		
		[DllImport("__Internal", EntryPoint="FullscreenWebGL_SetUnityFunctions")]
		private static extern void SetUnityFunctions(Action _onEnterFullscreenCallback, Action _onExitFullscreenCallback, Action _onFullscreenDetectedCallback, Action _onFullscreenNotDetectedCallback);
		
		public bool detectFullscreenOnStart = true;//if true, DetectFullscreen method is called when the app starts.
		public UnityEvent onEnterFullscreenCallback;//you can pass a callback to be called when your app goes fullscreen
		public UnityEvent onExitFullscreenCallback;//you can pass a callback to be called when your app exits fullscreen
		public UnityEvent onFullscreenDetectedCallback;//you can pass a callback to be called if your app is detected to be capable of going fullscreen
		public UnityEvent onFullscreenNotDetectedCallback;//you can pass a callback to be called if your app is detected to NOT be capable of going fullscreen
		
		private static UnityEvent onEnterFullscreenCallbackStatic;
		private static UnityEvent onExitFullscreenCallbackStatic;
		private static UnityEvent onFullscreenDetectedCallbackStatic;
		private static UnityEvent onFullscreenNotDetectedCallbackStatic;
		
		void Awake() {
			onEnterFullscreenCallbackStatic       = onEnterFullscreenCallback;
			onExitFullscreenCallbackStatic        = onExitFullscreenCallback;
			onFullscreenDetectedCallbackStatic    = onFullscreenDetectedCallback;
			onFullscreenNotDetectedCallbackStatic = onFullscreenNotDetectedCallback;
			
			#if UNITY_WEBGL && !UNITY_EDITOR
			SetUnityFunctions(_onEnterFullscreenCallback, _onExitFullscreenCallback, _onFullscreenDetectedCallback, _onFullscreenNotDetectedCallback);
			
			if (detectFullscreenOnStart)
				DetectFullscreen();
			#endif
		}

		[MonoPInvokeCallback(typeof(Action))]
		private static void _onEnterFullscreenCallback() {
			if (onEnterFullscreenCallbackStatic != null) {
				onEnterFullscreenCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onExitFullscreenCallback() {
			if (onExitFullscreenCallbackStatic != null) {
				onExitFullscreenCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onFullscreenDetectedCallback() {
			if (onFullscreenDetectedCallbackStatic != null) {
				onFullscreenDetectedCallbackStatic.Invoke();
			}
		}
		
		[MonoPInvokeCallback(typeof(Action))]
		private static void _onFullscreenNotDetectedCallback() {
			if (onFullscreenNotDetectedCallbackStatic != null) {
				onFullscreenNotDetectedCallbackStatic.Invoke();
			}
		}
	}
}