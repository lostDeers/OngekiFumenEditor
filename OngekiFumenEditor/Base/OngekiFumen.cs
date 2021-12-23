﻿using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Base
{
    public class OngekiFumen
    {
        public FumenMetaInfo MetaInfo { get; set; } = new FumenMetaInfo();
        public List<BulletPalleteList> BulletPalleteList { get; set; } = new List<BulletPalleteList>();
        public List<Bell> Bells { get; set; } = new List<Bell>();
        public BpmList BpmList { get; set; } = new BpmList();
        public List<MeterChange> MeterChanges { get; set; } = new List<MeterChange>();
        public List<EnemySet> EnemySets { get; set; } = new List<EnemySet>();

        #region Overload Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddObjects(params OngekiObjectBase[] objs) => AddObjects(objs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddObjects(IEnumerable<OngekiObjectBase> objs)
        {
            foreach (var item in objs)
            {
                AddObject(item);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveObjects(params OngekiObjectBase[] objs) => RemoveObjects(objs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveObjects(IEnumerable<OngekiObjectBase> objs)
        {
            foreach (var item in objs)
            {
                RemoveObject(item);
            }
        }
        #endregion

        public void Setup()
        {
            Bells.Sort();
            BpmList.Sort();
            MeterChanges.Sort();
            EnemySets.Sort();
        }

        public void AddObject(OngekiObjectBase obj)
        {
            if (obj is Bell bel)
            {
                Bells.Add(bel);
            }
            else if(obj is BPMChange bpm){
                BpmList.Add(bpm);
            }
            else if (obj is MeterChange met)
            {
                MeterChanges.Add(met);
            }
            else if (obj is EnemySet est)
            {
                EnemySets.Add(est);
            }
            else if (obj is BulletPalleteList bpl)
            {
                BulletPalleteList.Add(bpl);
            }
            else
            {
                Log.LogWarn($"add-in list target not found, object type : {obj?.GetType()?.Name}");
                return;
            }
        }

        public void RemoveObject(OngekiObjectBase obj)
        {
            if (obj is Bell bell)
            {
                Bells.Remove(bell);
            }
            else
            {
                Log.LogWarn($"remove list target not found, object type : {obj?.GetType()?.Name}");
                return;
            }
        }

        public IEnumerable<IDisplayableObject> GetAllDisplayableObjects()
        {
            return Bells.OfType<IDisplayableObject>();
        }

        public IEnumerable<BPMChange> GetAllBpmList()
        {
            return BpmList;
        }

        public string Serialize()
        {
            var sb = new StringBuilder();

            #region HEADER
            sb.AppendLine("[HEADER]");
            sb.AppendLine(MetaInfo.Serialize(this));
            #endregion

            #region B_PALETTE
            sb.AppendLine();
            sb.AppendLine("[B_PALETTE]");

            foreach (var bpl in BulletPalleteList.OrderBy(x=>x.StrID))
                sb.AppendLine(bpl.Serialize(this));
            #endregion

            #region COMPOSITION
            sb.AppendLine();
            sb.AppendLine("[COMPOSITION]");

            foreach (var o in BpmList.OrderBy(x => x.TGrid))
                sb.AppendLine(o.Serialize(this));
            sb.AppendLine();
            foreach (var o in MeterChanges.OrderBy(x => x.TGrid))
                sb.AppendLine(o.Serialize(this));
            foreach (var o in EnemySets.OrderBy(x => x.TGrid))
                sb.AppendLine(o.Serialize(this));
            #endregion

            #region BELL
            sb.AppendLine();
            sb.AppendLine("[BELL]");

            foreach (var u in Bells.OrderBy(x => x.TGrid))
                sb.AppendLine(u.Serialize(this));
            #endregion

            return sb.ToString();
        }
    }
}