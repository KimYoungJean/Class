using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class AttributeController : MonoBehaviour
{

    private void Start()
    {

        //ColorAttribute를 가진 필드를 찾는다.
        //BindingFlags : public이거나 private 상관 없이 static이 아닌 동적 할당 멤버만 탐색.
        BindingFlags bind = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        MonoBehaviour[] monoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            Type type = monoBehaviour.GetType();

            //콜렉션(배열, 리스트등)에서 특정 조건에 부합하는 요소만 가져오려 할경우,
            //foreach 또는 List.Find등 다소 복잡한 절차를 거쳐야 함.
            //List<FieldInfo> fields = new List<FieldInfo>(type.GetFields(bind));
            //fields.FindAll(null);

            //Linq 문법을 활용하여 이를 간소화 할 수 있음.
            //1. Linq에 정의된 확장 메서드 이용하는 방법
            IEnumerable<FieldInfo> colorAttachedFields = type.GetFields(bind).Where(field => field.GetCustomAttribute<ColorAttribute>() != null);

            //2. Linq를 통해 쿼리문과 비슷한 문법을 활용하는 방법
            colorAttachedFields = from field in type.GetFields(bind)
                                  where field.HasAttribute<ColorAttribute>()
                                  select field;

            foreach (FieldInfo field in colorAttachedFields)
            {
                ColorAttribute att = field.GetCustomAttribute<ColorAttribute>();
                object value = field.GetValue(monoBehaviour);

                if (value is Renderer rend)
                {
                    rend.material.color = att.color;
                }
                else if (value is Graphic graph)
                {
                    graph.color = att.color;
                }
                else
                {
                    Debug.LogError("저런, Color Attribute를 잘못 붙이셨네요 ㅎㅎ");
                }
            }

            IEnumerable<FieldInfo> sizeAttachedFields = from field in type.GetFields(bind)
                                                        where field.HasAttribute<SizeAttribute>()
                                                        select field;
            foreach (FieldInfo field in sizeAttachedFields)
            {
                SizeAttribute stt = field.GetCustomAttribute<SizeAttribute>();
                var SizeValue = field.GetValue(monoBehaviour);
               if(SizeValue is Graphic graph)
                {
                    if(stt.sizeType == SizeAttribute.SizeType.ScaleSize)
                    {
                        graph.rectTransform.localScale = new Vector3(stt.size, stt.size, stt.size);
                    }
                    else if(stt.sizeType == SizeAttribute.SizeType.RectSize)
                    graph.rectTransform.sizeDelta = new Vector2(stt.size, stt.size);                    
                }
               else if(SizeValue is Renderer rend)
                {
                    rend.transform.localScale = new Vector3(stt.size, stt.size, stt.size);
                }                
                else
                {
                    Debug.LogError("저런, Size Attribute를 잘못 붙이셨네요 ㅎㅎ");
                }
            }
            IEnumerable<FieldInfo> respawnAttachedFields = from field in type.GetFields(bind)
                                                           where field.HasAttribute<RespawnAttribute>()
                                                           select field;
            foreach (FieldInfo field in respawnAttachedFields)
                {
                RespawnAttribute rsp = field.GetCustomAttribute<RespawnAttribute>();
                var RespawnValue = field.GetValue(monoBehaviour);
                if (RespawnValue is GameObject go)
                {
                    go.transform.position = rsp.respawnPoint;
                }
                else
                {
                    Debug.LogError("저런, Respawn Attribute를 잘못 붙이셨네요 ㅎㅎ");
                }
            }
        }
    }
}

//Color를 조절할 수 있는 컴포넌트 또는 오브젝트에 [Color]라는 어트리뷰트를 붙여서 색을 설정하고 싶음

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ColorAttribute : Attribute
{
    public Color color;
    //public ColorAttribute(Color color) {//Attribute의 생성자에서는 리터럴 타입의 매개변수만 할당이 가능
    //}
    public ColorAttribute(float r = 0, float g = 0, float b = 0, float a = 1)
    {
        color = new Color(r, g, b, a);
    }
    public ColorAttribute()
    {
        color = Color.black;
    }
}

public static class AttributeHelper
{

    //특정 어트리뷰트를 가지고 있는지 여부만 확인하고 싶을때 쓸 확장 메서드
    public static bool HasAttribute<T>(this MemberInfo info) where T : Attribute
    {
        return info.GetCustomAttributes(typeof(T), true).Length > 0;
    }

}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class SizeAttribute : Attribute
{
    public enum SizeType
    {
        RectSize,
        ScaleSize
        
    }
    public int size;
    public SizeType sizeType;


    public SizeAttribute( SizeType sizeType=SizeType.ScaleSize,int size = 1)
    {
        this.sizeType = sizeType;
        this.size = size;
    }
    public SizeAttribute(int size =1)
    {
        this.size = size;
    }
   
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class RespawnAttribute : Attribute
{
    public Vector3 respawnPoint;

    public RespawnAttribute(float x, float y, float z)
    {
        respawnPoint = new Vector3(x, y, z);
    }
    public RespawnAttribute()
    {
        respawnPoint = new Vector3(4,4,4);
    }
}