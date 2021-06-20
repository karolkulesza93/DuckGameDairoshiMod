﻿namespace DuckGame.DairoshiMod
{
    [EditorGroup("DairoshiMod|Misc")]
    public class ClockOfDoom : Gun
    {
        public SpriteMap map;

        public ClockOfDoom(float xval, float yval) : base(xval, yval)
        {
            map = new SpriteMap(GetPath("clock"), 23, 23);
            map.AddAnimation("*tik*", 0.05f, true, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11);
            map.SetAnimation("*tik*");
            graphic = map;

            center = new Vec2(12.5f, 12.5f);
            collisionSize = new Vec2(23f, 23f);
            collisionOffset = new Vec2(-12.5f, -12.5f);
            _barrelOffsetTL = new Vec2(12.5f, 12.5f);
            _holdOffset = new Vec2(7f, 2f);

            ammo = 1;
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 0f;
            _fireSound = GetPath("stop");
            _ammoType = new ATDefault();
            editorTooltip = "Just like papa Frank once said: \"It\'s time to stop!\"";
        }

        public override void OnPressAction()
        {
            if (this.ammo > 0 && duck != null)
            {
                --this.ammo;
                duck.Swear();
                SFX.Play(GetPath("stop"));

                foreach (Thing t in Level.CheckCircleAll<Thing>(collisionCenter, 3000f))
                {
                    if (t is Duck && t != owner)
                    {
                        (t as Duck).Kill(new DTElectrocute(this));
                    }

                    if (t is Ragdoll && !(t as Ragdoll)._duck.dead)
                    {
                        (t as Ragdoll)._duck.Kill(new DTElectrocute(this));
                    }
                }
            }
        }

        public override void Fire()
        {
            // nothing here
        }
    }
}
