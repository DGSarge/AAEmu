﻿using System;
using System.Collections.Generic;
using System.Text;
using AAEmu.Game.Core.Managers;
using AAEmu.Game.Models.Game.Skills.Effects;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.Skills.Buffs.Triggers
{
    class TimeoutBuffTrigger : BuffTrigger
    {
        public override void Execute(object sender, EventArgs eventArgs)
        {
            var args = eventArgs as OnTimeoutArgs;
            _log.Warn("Buff[{0}] {1} executed. Applying {2}[{3}]!", _effect?.Template?.BuffId, this.GetType().Name, Template.Effect.GetType().Name, Template.Effect.Id);
            //Template.Effect.Apply()

            if (!(_owner is Unit owner))
            {
                _log.Warn("Owner is not a Unit");
                return;
            }

            var target = _effect.Owner;
            owner = (Unit)_effect.Owner;
            if (Template.EffectOnSource)
            {
                target = (Unit)_effect.Caster;
                //do what?
            }
            if (Template.UseOriginalSource)
            {
                owner = (Unit)_effect.Caster;
            }

            Template.Effect.Apply(owner, new SkillCasterUnit(_owner.ObjId), target, new SkillCastUnitTarget(target.ObjId), new CastBuff(_effect),
                new EffectSource(_effect?.Skill), // TODO : EffectSource Type trigger 
                null, DateTime.Now);
        }

        public TimeoutBuffTrigger(Effect owner, BuffTriggerTemplate template) : base(owner, template)
        {

        }
    }
}
