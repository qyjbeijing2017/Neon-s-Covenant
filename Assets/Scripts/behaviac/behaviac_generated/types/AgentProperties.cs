﻿// ---------------------------------------------------------------------
// THIS FILE IS AUTO-GENERATED BY BEHAVIAC DESIGNER, SO PLEASE DON'T MODIFY IT BY YOURSELF!
// ---------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace behaviac
{

	public class BehaviorLoaderImplement : BehaviorLoader
	{
		private class CMethod_behaviac_Agent_VectorAdd : CAgentMethodVoidBase
		{
			IInstanceMember _param0;
			IInstanceMember _param1;

			public CMethod_behaviac_Agent_VectorAdd()
			{
			}

			public CMethod_behaviac_Agent_VectorAdd(CMethod_behaviac_Agent_VectorAdd rhs) : base(rhs)
			{
			}

			public override IMethod Clone()
			{
				return new CMethod_behaviac_Agent_VectorAdd(this);
			}

			public override void Load(string instance, string[] paramStrs)
			{
				Debug.Check(paramStrs.Length == 2);

				_instance = instance;
				_param0 = AgentMeta.ParseProperty<IList>(paramStrs[0]);
				_param1 = AgentMeta.ParseProperty<System.Object>(paramStrs[1]);
			}

			public override void Run(Agent self)
			{
				Debug.Check(_param0 != null);
				Debug.Check(_param1 != null);

				behaviac.Agent.VectorAdd((IList)_param0.GetValueObject(self), (System.Object)_param1.GetValueObject(self));
			}
		}

		private class CMethod_behaviac_Agent_VectorClear : CAgentMethodVoidBase
		{
			IInstanceMember _param0;

			public CMethod_behaviac_Agent_VectorClear()
			{
			}

			public CMethod_behaviac_Agent_VectorClear(CMethod_behaviac_Agent_VectorClear rhs) : base(rhs)
			{
			}

			public override IMethod Clone()
			{
				return new CMethod_behaviac_Agent_VectorClear(this);
			}

			public override void Load(string instance, string[] paramStrs)
			{
				Debug.Check(paramStrs.Length == 1);

				_instance = instance;
				_param0 = AgentMeta.ParseProperty<IList>(paramStrs[0]);
			}

			public override void Run(Agent self)
			{
				Debug.Check(_param0 != null);

				behaviac.Agent.VectorClear((IList)_param0.GetValueObject(self));
			}
		}

		private class CMethod_behaviac_Agent_VectorContains : CAgentMethodBase<bool>
		{
			IInstanceMember _param0;
			IInstanceMember _param1;

			public CMethod_behaviac_Agent_VectorContains()
			{
			}

			public CMethod_behaviac_Agent_VectorContains(CMethod_behaviac_Agent_VectorContains rhs) : base(rhs)
			{
			}

			public override IMethod Clone()
			{
				return new CMethod_behaviac_Agent_VectorContains(this);
			}

			public override void Load(string instance, string[] paramStrs)
			{
				Debug.Check(paramStrs.Length == 2);

				_instance = instance;
				_param0 = AgentMeta.ParseProperty<IList>(paramStrs[0]);
				_param1 = AgentMeta.ParseProperty<System.Object>(paramStrs[1]);
			}

			public override void Run(Agent self)
			{
				Debug.Check(_param0 != null);
				Debug.Check(_param1 != null);

				_returnValue.value = behaviac.Agent.VectorContains((IList)_param0.GetValueObject(self), (System.Object)_param1.GetValueObject(self));
			}
		}

		private class CMethod_behaviac_Agent_VectorLength : CAgentMethodBase<int>
		{
			IInstanceMember _param0;

			public CMethod_behaviac_Agent_VectorLength()
			{
			}

			public CMethod_behaviac_Agent_VectorLength(CMethod_behaviac_Agent_VectorLength rhs) : base(rhs)
			{
			}

			public override IMethod Clone()
			{
				return new CMethod_behaviac_Agent_VectorLength(this);
			}

			public override void Load(string instance, string[] paramStrs)
			{
				Debug.Check(paramStrs.Length == 1);

				_instance = instance;
				_param0 = AgentMeta.ParseProperty<IList>(paramStrs[0]);
			}

			public override void Run(Agent self)
			{
				Debug.Check(_param0 != null);

				_returnValue.value = behaviac.Agent.VectorLength((IList)_param0.GetValueObject(self));
			}
		}

		private class CMethod_behaviac_Agent_VectorRemove : CAgentMethodVoidBase
		{
			IInstanceMember _param0;
			IInstanceMember _param1;

			public CMethod_behaviac_Agent_VectorRemove()
			{
			}

			public CMethod_behaviac_Agent_VectorRemove(CMethod_behaviac_Agent_VectorRemove rhs) : base(rhs)
			{
			}

			public override IMethod Clone()
			{
				return new CMethod_behaviac_Agent_VectorRemove(this);
			}

			public override void Load(string instance, string[] paramStrs)
			{
				Debug.Check(paramStrs.Length == 2);

				_instance = instance;
				_param0 = AgentMeta.ParseProperty<IList>(paramStrs[0]);
				_param1 = AgentMeta.ParseProperty<System.Object>(paramStrs[1]);
			}

			public override void Run(Agent self)
			{
				Debug.Check(_param0 != null);
				Debug.Check(_param1 != null);

				behaviac.Agent.VectorRemove((IList)_param0.GetValueObject(self), (System.Object)_param1.GetValueObject(self));
			}
		}


		public override bool Load()
		{
			AgentMeta.TotalSignature = 3495682737;

			AgentMeta meta;

			// behaviac.Agent
			meta = new AgentMeta(24743406);
			AgentMeta._AgentMetas_[2436498804] = meta;
			meta.RegisterMethod(1045109914, new CAgentStaticMethodVoid<string>(delegate(string param0) { behaviac.Agent.LogMessage(param0); }));
			meta.RegisterMethod(2521019022, new CMethod_behaviac_Agent_VectorAdd());
			meta.RegisterMethod(2306090221, new CMethod_behaviac_Agent_VectorClear());
			meta.RegisterMethod(3483755530, new CMethod_behaviac_Agent_VectorContains());
			meta.RegisterMethod(505785840, new CMethod_behaviac_Agent_VectorLength());
			meta.RegisterMethod(502968959, new CMethod_behaviac_Agent_VectorRemove());

			// AgentBase
			meta = new AgentMeta(106096405);
			AgentMeta._AgentMetas_[276212682] = meta;
			meta.RegisterMemberProperty(2375559887, new CMemberProperty<float>("attackFlySpeed", delegate(Agent self, float value) { ((AgentBase)self).attackFlySpeed = value; }, delegate(Agent self) { return ((AgentBase)self).attackFlySpeed; }));
			meta.RegisterMemberProperty(495703176, new CMemberProperty<float>("attackFreq", delegate(Agent self, float value) { ((AgentBase)self).attackFreq = value; }, delegate(Agent self) { return ((AgentBase)self).attackFreq; }));
			meta.RegisterMemberProperty(1183580410, new CMemberProperty<float>("attackValueDistant", delegate(Agent self, float value) { ((AgentBase)self).attackValueDistant = value; }, delegate(Agent self) { return ((AgentBase)self).attackValueDistant; }));
			meta.RegisterMemberProperty(3044374240, new CMemberProperty<float>("attackValueNear", delegate(Agent self, float value) { ((AgentBase)self).attackValueNear = value; }, delegate(Agent self) { return ((AgentBase)self).attackValueNear; }));
			meta.RegisterMemberProperty(220810531, new CMemberProperty<float>("dashDistance", delegate(Agent self, float value) { ((AgentBase)self).dashDistance = value; }, delegate(Agent self) { return ((AgentBase)self).dashDistance; }));
			meta.RegisterMemberProperty(1097215746, new CMemberProperty<float>("moveSpeed", delegate(Agent self, float value) { ((AgentBase)self).moveSpeed = value; }, delegate(Agent self) { return ((AgentBase)self).moveSpeed; }));
			meta.RegisterMemberProperty(1832315759, new CMemberProperty<float>("rotateSpeed", delegate(Agent self, float value) { ((AgentBase)self).rotateSpeed = value; }, delegate(Agent self) { return ((AgentBase)self).rotateSpeed; }));
			meta.RegisterMethod(214051370, new CAgentMethodVoid<int, float, int>(delegate(Agent self, int color, float value, int typeOfAttack) { ((AgentBase)self).GetHit(color, value, typeOfAttack); }));
			meta.RegisterMethod(1045109914, new CAgentStaticMethodVoid<string>(delegate(string param0) { AgentBase.LogMessage(param0); }));
			meta.RegisterMethod(3042982998, new CAgentMethodVoid(delegate(Agent self) { ((AgentBase)self).Move(); }));
			meta.RegisterMethod(1505908390, new CAgentMethodVoid(delegate(Agent self) { ((AgentBase)self).SayHello(); }));
			meta.RegisterMethod(4204852309, new CAgentMethodVoid(delegate(Agent self) { ((AgentBase)self).SayMyName(); }));
			meta.RegisterMethod(2521019022, new CMethod_behaviac_Agent_VectorAdd());
			meta.RegisterMethod(2306090221, new CMethod_behaviac_Agent_VectorClear());
			meta.RegisterMethod(3483755530, new CMethod_behaviac_Agent_VectorContains());
			meta.RegisterMethod(505785840, new CMethod_behaviac_Agent_VectorLength());
			meta.RegisterMethod(502968959, new CMethod_behaviac_Agent_VectorRemove());

			// BossAgent
			meta = new AgentMeta(2125627704);
			AgentMeta._AgentMetas_[1771554173] = meta;
			meta.RegisterMemberProperty(2375559887, new CMemberProperty<float>("attackFlySpeed", delegate(Agent self, float value) { ((BossAgent)self).attackFlySpeed = value; }, delegate(Agent self) { return ((BossAgent)self).attackFlySpeed; }));
			meta.RegisterMemberProperty(495703176, new CMemberProperty<float>("attackFreq", delegate(Agent self, float value) { ((BossAgent)self).attackFreq = value; }, delegate(Agent self) { return ((BossAgent)self).attackFreq; }));
			meta.RegisterMemberProperty(1183580410, new CMemberProperty<float>("attackValueDistant", delegate(Agent self, float value) { ((BossAgent)self).attackValueDistant = value; }, delegate(Agent self) { return ((BossAgent)self).attackValueDistant; }));
			meta.RegisterMemberProperty(3044374240, new CMemberProperty<float>("attackValueNear", delegate(Agent self, float value) { ((BossAgent)self).attackValueNear = value; }, delegate(Agent self) { return ((BossAgent)self).attackValueNear; }));
			meta.RegisterMemberProperty(2353983445, new CMemberProperty<bool>("bActivated", delegate(Agent self, bool value) { ((BossAgent)self)._set_bActivated(value); }, delegate(Agent self) { return ((BossAgent)self)._get_bActivated(); }));
			meta.RegisterMemberProperty(220810531, new CMemberProperty<float>("dashDistance", delegate(Agent self, float value) { ((BossAgent)self).dashDistance = value; }, delegate(Agent self) { return ((BossAgent)self).dashDistance; }));
			meta.RegisterMemberProperty(2342986778, new CMemberProperty<bool>("isActing", delegate(Agent self, bool value) { ((BossAgent)self)._set_isActing(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isActing(); }));
			meta.RegisterMemberProperty(1450589284, new CMemberProperty<bool>("isAttacking", delegate(Agent self, bool value) { ((BossAgent)self)._set_isAttacking(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isAttacking(); }));
			meta.RegisterMemberProperty(974135787, new CMemberProperty<bool>("isAttackingNear", delegate(Agent self, bool value) { ((BossAgent)self)._set_isAttackingNear(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isAttackingNear(); }));
			meta.RegisterMemberProperty(3443514664, new CMemberProperty<bool>("isChasing", delegate(Agent self, bool value) { ((BossAgent)self)._set_isChasing(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isChasing(); }));
			meta.RegisterMemberProperty(3139587129, new CMemberProperty<bool>("isLooking", delegate(Agent self, bool value) { ((BossAgent)self)._set_isLooking(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isLooking(); }));
			meta.RegisterMemberProperty(1371534460, new CMemberProperty<bool>("isWeak", delegate(Agent self, bool value) { ((BossAgent)self)._set_isWeak(value); }, delegate(Agent self) { return ((BossAgent)self)._get_isWeak(); }));
			meta.RegisterMemberProperty(1097215746, new CMemberProperty<float>("moveSpeed", delegate(Agent self, float value) { ((BossAgent)self).moveSpeed = value; }, delegate(Agent self) { return ((BossAgent)self).moveSpeed; }));
			meta.RegisterMemberProperty(1832315759, new CMemberProperty<float>("rotateSpeed", delegate(Agent self, float value) { ((BossAgent)self).rotateSpeed = value; }, delegate(Agent self) { return ((BossAgent)self).rotateSpeed; }));
			meta.RegisterMemberProperty(1895258509, new CMemberProperty<int>("State", delegate(Agent self, int value) { ((BossAgent)self)._set_State(value); }, delegate(Agent self) { return ((BossAgent)self)._get_State(); }));
			meta.RegisterMemberProperty(2615510322, new CMemberProperty<float>("timeToDecide", delegate(Agent self, float value) { ((BossAgent)self)._set_timeToDecide(value); }, delegate(Agent self) { return ((BossAgent)self)._get_timeToDecide(); }));
			meta.RegisterMemberProperty(1421501087, new CMemberProperty<float>("weakCD", delegate(Agent self, float value) { ((BossAgent)self)._set_weakCD(value); }, delegate(Agent self) { return ((BossAgent)self)._get_weakCD(); }));
			meta.RegisterMethod(3818425727, new CAgentMethodVoid(delegate(Agent self) { ((BossAgent)self).AttackNear1(); }));
			meta.RegisterMethod(2036582810, new CAgentMethodVoid(delegate(Agent self) { ((BossAgent)self).BossEnable(); }));
			meta.RegisterMethod(214051370, new CAgentMethodVoid<int, float, int>(delegate(Agent self, int color, float value, int typeOfAttack) { ((BossAgent)self).GetHit(color, value, typeOfAttack); }));
			meta.RegisterMethod(3026905997, new CAgentMethod<bool>(delegate(Agent self) { return ((BossAgent)self).IsNearCharacter(); }));
			meta.RegisterMethod(1045109914, new CAgentStaticMethodVoid<string>(delegate(string param0) { BossAgent.LogMessage(param0); }));
			meta.RegisterMethod(3042982998, new CAgentMethodVoid(delegate(Agent self) { ((BossAgent)self).Move(); }));
			meta.RegisterMethod(1505908390, new CAgentMethodVoid(delegate(Agent self) { ((BossAgent)self).SayHello(); }));
			meta.RegisterMethod(4204852309, new CAgentMethodVoid(delegate(Agent self) { ((BossAgent)self).SayMyName(); }));
			meta.RegisterMethod(2521019022, new CMethod_behaviac_Agent_VectorAdd());
			meta.RegisterMethod(2306090221, new CMethod_behaviac_Agent_VectorClear());
			meta.RegisterMethod(3483755530, new CMethod_behaviac_Agent_VectorContains());
			meta.RegisterMethod(505785840, new CMethod_behaviac_Agent_VectorLength());
			meta.RegisterMethod(502968959, new CMethod_behaviac_Agent_VectorRemove());

			AgentMeta.Register<behaviac.Agent>("behaviac.Agent");
			AgentMeta.Register<AgentBase>("AgentBase");
			AgentMeta.Register<BossAgent>("BossAgent");
			return true;
		}

		public override bool UnLoad()
		{
			AgentMeta.UnRegister<behaviac.Agent>("behaviac.Agent");
			AgentMeta.UnRegister<AgentBase>("AgentBase");
			AgentMeta.UnRegister<BossAgent>("BossAgent");
			return true;
		}
	}
}
