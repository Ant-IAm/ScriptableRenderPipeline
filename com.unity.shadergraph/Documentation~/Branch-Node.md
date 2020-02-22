# Branch Node

## Description

Provides a dynamic branch to the shader. If input **Predicate** is true, the return output is equal to input **True**. Otherwise, it is equal to input **False**. This is determined per vertex or per pixel depending on the shader stage. Both sides of the branch are evaluated in the shader, and the unused branch is discarded.

## Ports

| Name        | Direction           | Type  | Binding | Description |
|:------------ |:-------------|:-----|:---|:---|
| Predicate      | Input | Boolean | None | Determines which input to returned |
| True     | Input | Dynamic Vector | None | Returned if **Predicate** is true |
| False      | Input | Dynamic Vector | None | Returned if **Predicate** is false |
| Out | Output      |    Boolean | None | Output value |

## Generated Code Example

The following example code represents one possible outcome of this node.

```
void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
{
    Out = Predicate ? True : False;
}
```
