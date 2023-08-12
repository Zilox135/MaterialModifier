# MaterialModifier
A mod for 7 days to die that allows entity materials to be tinted and replaced.

Example xml. 

Shader Properties can be found in unity on the materials Shader, you can specify one, or multiple.

Tint the whole thing
    <property name="TintColor" value="FCFF00"/>
    <property name="TintShaderProperties" value="_Color,_TintColor,_EmissiveColor"/>


Tint by material index.
    <property name="TintMaterial0" value="#b00202"/>
    <property name="TintMaterial1" value="#b00202"/>
    <property name="TintMaterial2" value="#b00202"/>
    <property name="TintMaterial3" value="#b00202"/>
    <property name="TintMaterial4" value="#b00202"/>
    <property name="TintShaderProperties" value="_Color,_TintColor,_EmissiveColor"/>

Replace Materials by name.
	<triggered_effect trigger="onSelfFirstSpawn" action="ReplaceMaterial, MaterialModifier" target_material_name="iron_age_cattle_female" replace_material="#@modfolder:Resources/Animals.unity3d?iron_age_cattle_male_mat.prefab"/>
	
Additional Notes.	
    <property name="TintColor" value="#550373"/> 
    <property name="TintShaderProperties" value="_EmissionColor"/>
    <!-- _EmissionColor is for unity shader while _EmissiveColor is for the TFP shader
    <property name="TintShaderProperties" value="_Color,_TintColor,_EmissiveColor,_EmissionColor"/> -->		