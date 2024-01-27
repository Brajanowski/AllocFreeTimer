using System;
using TMPro;
using UnityEngine;

namespace AllocFreeTimer
{
    public class TimerText : MonoBehaviour
    {
        private const byte ShowHoursFlag = 8;
        private const byte ShowMinutesFlag = 4;
        private const byte ShowSecondsFlag = 2;
        private const byte ShowMillisecondsFlag = 1;

        public enum TimerFormat : byte
        {
            HHMMSSFFF = ShowHoursFlag | ShowMinutesFlag | ShowSecondsFlag | ShowMillisecondsFlag,
            MMSSFFF = ShowMinutesFlag | ShowSecondsFlag | ShowMillisecondsFlag,
            SSFFF = ShowSecondsFlag | ShowMillisecondsFlag,
            HHMMSS = ShowHoursFlag | ShowMinutesFlag | ShowSecondsFlag,
            MMSS = ShowMinutesFlag | ShowSecondsFlag,
            SS = ShowSecondsFlag
        }

        [SerializeField]
        private TimerFormat _format = TimerFormat.HHMMSSFFF;

        [SerializeField]
        private TMP_Text _text;

        // max characters: hh:mm:ss:mmm
        private char[] _buffer = new char[3 + 3 + 3 + 3];

        public void SetSeconds(float totalSeconds)
        {
            const int numbersStart = 48;

            int totalSecondsInt = (int) totalSeconds;
            int index = 0;

            byte flags = (byte) _format;

            if ((flags & ShowHoursFlag) == ShowHoursFlag)
            {
                int hours = totalSecondsInt / 3600;
                _buffer[index++] = (char) (numbersStart + hours / 10);
                _buffer[index++] = (char) (numbersStart + hours % 10);
            }

            if ((flags & ShowMinutesFlag) == ShowMinutesFlag)
            {
                if ((flags & ShowHoursFlag) == ShowHoursFlag)
                {
                    _buffer[index++] = ':';
                }

                int minutes = totalSecondsInt / 60 % 60;
                _buffer[index++] = (char) (numbersStart + minutes / 10);
                _buffer[index++] = (char) (numbersStart + minutes % 10);
            }

            if ((flags & ShowSecondsFlag) == ShowSecondsFlag)
            {
                if ((flags & ShowMinutesFlag) == ShowMinutesFlag)
                {
                    _buffer[index++] = ':';
                }

                int seconds = totalSecondsInt % 60;

                _buffer[index++] = (char) (numbersStart + seconds / 10);
                _buffer[index++] = (char) (numbersStart + seconds % 10);
            }

            if ((flags & ShowMillisecondsFlag) == ShowMillisecondsFlag)
            {
                _buffer[index++] = '.';

                int milliseconds = (int) (1000 * (totalSeconds - Mathf.Floor(totalSeconds)));

                _buffer[index++] = (char) (numbersStart + milliseconds / 100);
                _buffer[index++] = (char) (numbersStart + milliseconds / 10 % 10);
                _buffer[index++] = (char) (numbersStart + milliseconds % 10);
            }

            _text.SetCharArray(_buffer, 0, index);
        }

        public void SetTimespan(TimeSpan timeSpan)
        {
            SetSeconds((float) timeSpan.TotalSeconds);
        }

        public void SetFormat(TimerFormat format)
        {
            _format = format;
        }
    }
}
