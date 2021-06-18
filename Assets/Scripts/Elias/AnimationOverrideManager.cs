using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts.AnimationManagement
{
    /// <summary>
    /// Stores animation clips that will override existing clips. Source: https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html
    /// </summary>
    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));

                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }

    /// <summary>
    /// Manager for overriding Animation Clips and Animators. Source: https://docs.unity3d.com/ScriptReference/AnimatorOverrideController.html
    /// </summary>
    public class AnimationOverrideManager
    {
        #region Variables
        Animator _Animator = null;
        AnimatorOverrideController _AnimOverrideController;
        AnimationClipOverrides _AnimClipOverrides;
        #endregion

        #region Get Set
        public Animator Animator 
        { 
            get => _Animator; 
            set 
            { 
                _Animator = value;
                _AnimOverrideController = new AnimatorOverrideController(_Animator.runtimeAnimatorController);
                Animator.runtimeAnimatorController = _AnimOverrideController;

                _AnimClipOverrides = new AnimationClipOverrides(_AnimOverrideController.overridesCount);
                _AnimOverrideController.GetOverrides(_AnimClipOverrides);
            } 
        }
        #endregion

        #region Constructors
        public AnimationOverrideManager() { }

        public AnimationOverrideManager(Animator animator)
        {
            _AnimOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            Animator.runtimeAnimatorController = _AnimOverrideController;

            _AnimClipOverrides = new AnimationClipOverrides(_AnimOverrideController.overridesCount);
            _AnimOverrideController.GetOverrides(_AnimClipOverrides);
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Overrides a base animation clip from the animator to a new animation clip.
        /// </summary>
        /// <param name="animationClip"></param>
        public void OverrideAnimationClip(string baseAnimClip, AnimationClip overrideAnimClip)
        {
            if (_Animator == null)
            {
                Debug.LogError("AnimationOverrideManager Error: _Animator is null!");
                return;
            }

            _AnimClipOverrides[baseAnimClip] = overrideAnimClip;
            _AnimOverrideController.ApplyOverrides(_AnimClipOverrides);
        }

        /// <summary>
        /// Overrides a base animation clip from the animator to a new animation clip.
        /// </summary>
        /// <param name="animationClip"></param>
        public void OverrideAnimationClip(string[] baseAnimClip, AnimationClip overrideAnimClip)
        {
            if(_Animator == null)
            {
                Debug.LogError("AnimationOverrideManager Error: _Animator is null!");
                return;
            }

            foreach (string clip in baseAnimClip)
                _AnimClipOverrides[clip] = overrideAnimClip;

            _AnimOverrideController.ApplyOverrides(_AnimClipOverrides);
        }
        #endregion
    }
}