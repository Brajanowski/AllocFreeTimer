# About
Simple script to add allocation free text timers to your games, for example level timer or in-game clock.

### Supported formats:
- HH:MM:SS.FFF
- MM:SS.FFF
- SS:FFF
- HH:MM:SS
- MM:SS
- SS

# Why
Strings are immutable and every time when we need to update .text property we have to allocate new string.
This script uses TMP_Text.SetCharArray() to avoid unnecessary allocations. 

### NOTE
The editor will still allocate memory, because TextMeshPro will generate a new string to update the inspector.

```csharp
public void SetCharArray(char[] sourceText, int start, int length)
{
    PopulateTextBackingArray(sourceText, start, length);

    m_IsTextBackingStringDirty = true;

    #if UNITY_EDITOR
    m_text = InternalTextBackingArrayToString(); // Allocation happens here
    #endif

    // Set input source
    m_inputSource = TextInputSources.SetTextArray;

    PopulateTextProcessingArray();

    m_havePropertiesChanged = true;

    SetVerticesDirty();
    SetLayoutDirty();
}
```

# Installation
Just copy TimerText.cs somewhere to your project. For example here: Assets/Scripts/TimerText.cs

# Usage
Basically you have two ways to update text value

by passing total seconds:
```csharp
void SetSeconds(float totalSeconds);
```
or TimeSpan:
```csharp
void SetTimespan(TimeSpan timeSpan)
```

