﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7CommPlusDriver.ClientApi
{
    public class PlcTags
    {
        List<PlcTag> m_Tags = new List<PlcTag>();

        public void AddTag(PlcTag tag)
        {
            m_Tags.Add(tag);
        }

        public int ReadTags(S7CommPlusConnection conn)
        {
            var readlist = new List<ItemAddress>();
            List<object> values;
            List<UInt64> errors;
            int res;

            foreach (var tag in m_Tags)
            {
                readlist.Add(tag.Address);
            }

            res = conn.ReadValues(readlist, out values, out errors);

            if (res == 0)
            {
                for (int i = 0; i < readlist.Count; i++)
                {
                    m_Tags[i].ProcessReadResult(values[i], errors[i]);
                }
            } 
            else
            {
                Console.WriteLine("ReadTags: Error res=" + res);
            }
            return res;
        }

        public int WriteTags(S7CommPlusConnection conn)
        {
            var writelist = new List<ItemAddress>();
            var values = new List<PValue>();
            List<UInt64> errors;
            int res;

            foreach (var tag in m_Tags)
            {
                writelist.Add(tag.Address);
                values.Add(tag.GetWriteValue());
            }

            res = conn.WriteValues(writelist, values, out errors);

            if (res == 0)
            {
                for (int i = 0; i < writelist.Count; i++)
                {
                    m_Tags[i].ProcessWriteResult(errors[i]);
                }
            }
            else
            {
                Console.WriteLine("WriteTags: Error res=" + res);
            }
            return res;
        }

        public PlcTag TagFactory(string name, ItemAddress address, uint softdatatype)
        {
            switch (softdatatype)
            {
                case Softdatatype.S7COMMP_SOFTDATATYPE_BOOL:
                    return new PlcTagBool(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_BYTE:
                    return new PlcTagByte(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_CHAR:
                    return new PlcTagChar(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_WORD:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_INT:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DWORD:
                    return new PlcTagDWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DINT:
                    return new PlcTagDInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_REAL:
                    return new PlcTagReal(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DATE:
                    return new PlcTagDate(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_TIMEOFDAY:
                    return new PlcTagTimeOfDay(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_TIME:
                    return new PlcTagTime(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_S5TIME:
                    return new PlcTagS5Time(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DATEANDTIME:
                    return new PlcTagDateAndTime(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_STRING:
                    return new PlcTagString(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_POINTER:
                    return new PlcTagPointer(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_ANY:
                    return new PlcTagAny(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_BLOCKFB:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_BLOCKFC:
                    return new PlcTagUInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_COUNTER:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_TIMER:
                    return new PlcTagUInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_BBOOL:
                    return new PlcTagBool(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_LREAL:
                    return new PlcTagLReal(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_ULINT:
                    return new PlcTagULInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_LINT:
                    return new PlcTagLInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_LWORD:
                    return new PlcTagLWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_USINT:
                    return new PlcTagUSInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_UINT:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_UDINT:
                    return new PlcTagUDInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_SINT:
                    return new PlcTagSInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_WCHAR:
                    return new PlcTagWChar(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_WSTRING:
                    return new PlcTagWString(name, address, softdatatype);
                //case Softdatatype.S7COMMP_SOFTDATATYPE_VARIANT:
                //-> Variant isn't added int the instance-db as a variable!
                case Softdatatype.S7COMMP_SOFTDATATYPE_LTIME:
                    return new PlcTagLTime(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_LTOD:
                    return new PlcTagLTOD(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_LDT:
                    return new PlcTagLDT(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DTL:
                    return new PlcTagDTL(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_REMOTE:
                    return new PlcTagAny(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_AOMIDENT:
                    return new PlcTagDWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_EVENTANY:
                    return new PlcTagDWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_EVENTATT:
                    return new PlcTagDWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_FOLDER:
                    // Softdatatype 132: This type is only used as parameter for internal SFBs (e.g. AID input parameter)
                    // Length of value (4 byte) calculated from the offsetinfo byte addresses.
                    return new PlcTagDWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_AOMLINK:
                    return new PlcTagDWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_HWANY:
                    return new PlcTagWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_HWIOSYSTEM:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWDPMASTER:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWDEVICE:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWDPSLAVE:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWIO:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWMODULE:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWSUBMODULE:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWHSC:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWPWM:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWPTO:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWINTERFACE:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_HWIEPORT:
                    return new PlcTagWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_OBANY:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBDELAY:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBTOD:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBCYCLIC:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBATT:
                    return new PlcTagInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_CONNANY:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_CONNPRG:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_CONNOUC:
                    return new PlcTagWord(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_CONNRID:
                    return new PlcTagDWord(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_PORT:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_RTM:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_PIP:
                    return new PlcTagUInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_OBPCYCLE:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBHWINT:
                    return new PlcTagInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_OBDIAG:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBTIMEERROR:
                    return new PlcTagInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_OBSTARTUP:
                    return new PlcTagInt(name, address, softdatatype);

                case Softdatatype.S7COMMP_SOFTDATATYPE_DBANY:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DBWWW:
                    return new PlcTagUInt(name, address, softdatatype);
                case Softdatatype.S7COMMP_SOFTDATATYPE_DBDYN:
                    return new PlcTagUInt(name, address, softdatatype);

                default:
                    Console.WriteLine("ERROR: Unknown softdatatype=" + softdatatype.ToString() + " for variable= " + name);
                    return null;
            }
        }
    }
}
