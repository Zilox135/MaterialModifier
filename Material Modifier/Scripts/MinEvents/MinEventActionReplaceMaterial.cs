using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MinEventActionReplaceMaterial : MinEventActionTargetedBase
{
    private string _targetMaterialName = string.Empty;
    private string _replaceMaterialPath = string.Empty;

    private Renderer[] _renderers;
    private Material[] _materials;

    public override void Execute(MinEventParams _params)
    {
        _renderers = _params.Self?.RootTransform?.GetComponentsInChildren<Renderer>();

        if (_renderers == null || _renderers.Length == 0)
        {
            return;
        }

        foreach (Renderer renderer in _renderers)
        {
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                _materials = renderer.materials;
                
                if (_materials[i] == null || string.IsNullOrEmpty(_materials[i].name))
                {
                    continue;
                }

                string currentMaterialName = _materials[i].name;

                if (_materials[i].name.EndsWith("(Instance)"))
                {
                    currentMaterialName = _materials[i].name.Replace("(Instance)", string.Empty).Trim();
                }

                if (currentMaterialName == _targetMaterialName)
                {
                    Material replaceMaterial = DataLoader.LoadAsset<Material>(_replaceMaterialPath);

                    if (replaceMaterial == null)
                    {
                        Log.Warning($"Failed to replace target material '{_targetMaterialName}' with '{_replaceMaterialPath}' because it could not be loaded!");
                        return;
                    }

                    _materials[i] = replaceMaterial;
                    renderer.materials = _materials;
                }
            }
        }
    }

    public override bool CanExecute(MinEventTypes _eventType, MinEventParams _params)
    {
        return base.CanExecute(_eventType, _params) && _params.Self != null && _params.Self.world != null;
    }

    public override bool ParseXmlAttribute(XAttribute _attribute)
    {
        bool flag = base.ParseXmlAttribute(_attribute);
        if (!flag)
        {
            string localName = _attribute.Name.LocalName;
            if (localName == "target_material_name")
            {
                _targetMaterialName = _attribute.Value;
                return true;
            }

            if (localName == "replace_material")
            {
                _replaceMaterialPath = _attribute.Value;
                DataLoader.PreloadBundle(_replaceMaterialPath);
            }
        }
        return flag;
    }
}
