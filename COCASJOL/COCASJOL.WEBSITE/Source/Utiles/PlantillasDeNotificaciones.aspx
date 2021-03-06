﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlantillasDeNotificaciones.aspx.cs" Inherits="COCASJOL.WEBSITE.Source.Utiles.PlantillasDeNotificaciones" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Src="~/Source/Auditoria/Auditoria.ascx" TagPrefix="aud" TagName="Auditoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Plantillas de Notificaciones</title>
    <script type="text/javascript" src="../../resources/js/utiles/plantillasDeNotificaciones.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
            <Listeners>
                <DocumentReady Handler="PageX.setReferences();" />
            </Listeners>
        </ext:ResourceManager>

        <ext:KeyNav ID="KeyNav1" runat="server" Target="={document.body}" >
            <Home Handler="PageX.navHome();" />
            <PageUp Handler="PageX.navPrev();" />
            <PageDown Handler="PageX.navNext();" />
            <End Handler="PageX.navEnd();" />
        </ext:KeyNav>

        <aud:Auditoria runat="server" ID="AudWin" />

        <asp:ObjectDataSource ID="PlantillaDS" runat="server"
                TypeName="COCASJOL.LOGIC.Utiles.PlantillaLogic"
                SelectMethod="GetPlantillas"
                UpdateMethod="ActualizarPlantilla" onselecting="PlantillaDS_Selecting" >
                <SelectParameters>
                    <asp:ControlParameter Name="PLANTILLAS_LLAVE"   Type="String"    ControlID="f_PLANTILLAS_LLAVE"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="PLANTILLAS_NOMBRE"  Type="String"    ControlID="f_PLANTILLAS_NOMBRE" PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="PLANTILLAS_ASUNTO"  Type="String"   ControlID="f_PLANTILLAS_ASUNTO"  PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="PLANTILLAS_MENSAJE" Type="String"   ControlID="nullHdn"              PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="CREADO_POR"         Type="String"   ControlID="nullHdn"              PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_CREACION"     Type="DateTime" ControlID="nullHdn"              PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="MODIFICADO_POR"     Type="String"   ControlID="nullHdn"              PropertyName="Text" DefaultValue="" />
                    <asp:ControlParameter Name="FECHA_MODIFICACION" Type="DateTime" ControlID="nullHdn"              PropertyName="Text" DefaultValue="" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="PLANTILLAS_LLAVE"    Type="String" />
                    <asp:Parameter Name="PLANTILLAS_NOMBRE"   Type="String" />
                    <asp:Parameter Name="PLANTILLAS_ASUNTO"   Type="String" />
                    <asp:Parameter Name="PLANTILLAS_MENSAJE"  Type="String" />
                    <asp:Parameter Name="CREADO_POR"          Type="String" />
                    <asp:Parameter Name="FECHA_CREACION"      Type="DateTime" />
                    <asp:Parameter Name="MODIFICADO_POR"      Type="String" />
                    <asp:Parameter Name="FECHA_MODIFICACION"  Type="DateTime" />
                </UpdateParameters>
        </asp:ObjectDataSource>
        
        <ext:Hidden ID="nullHdn" runat="server" >
        </ext:Hidden>

        <ext:Hidden ID="LoggedUserHdn" runat="server" >
        </ext:Hidden>

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Frame="false" Header="false" Title="Productos" Icon="Basket" Layout="Fit">
                    <Items>
                        <ext:GridPanel ID="PlantillasGridP" runat="server" AutoExpandColumn="PLANTILLAS_NOMBRE" Height="300"
                            Title="Plantillas de Notificaciones" Header="false" Border="false" StripeRows="true" TrackMouseOver="true">
                            <KeyMap>
                                <ext:KeyBinding Ctrl="true" >
                                    <Keys>
                                        <ext:Key Code="ENTER" />
                                    </Keys>
                                    <Listeners>
                                        <Event Handler="PageX.gridKeyUpEvent(this, e);" />
                                    </Listeners>
                                </ext:KeyBinding>
                            </KeyMap>
                            <Store>
                                <ext:Store ID="PlantillasSt" runat="server" DataSourceID="PlantillaDS" AutoSave="true" SkipIdForNewRecords="false" >
                                    <Reader>
                                        <ext:JsonReader IDProperty="PLANTILLAS_LLAVE">
                                            <Fields>
                                                <ext:RecordField Name="PLANTILLAS_LLAVE"   />
                                                <ext:RecordField Name="PLANTILLAS_NOMBRE"  />
                                                <ext:RecordField Name="PLANTILLAS_ASUNTO"  />
                                                <ext:RecordField Name="PLANTILLAS_MENSAJE" />
                                                <ext:RecordField Name="CREADO_POR"         />
                                                <ext:RecordField Name="FECHA_CREACION"     Type="Date" />
                                                <ext:RecordField Name="MODIFICADO_POR"     />
                                                <ext:RecordField Name="FECHA_MODIFICACION" Type="Date" />
                                            </Fields>
                                        </ext:JsonReader>
                                    </Reader>
                                    <Listeners>
                                        <CommitDone Handler="Ext.Msg.alert('Guardar', 'Cambios guardados exitosamente.');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column DataIndex="PLANTILLAS_LLAVE"   Header="Llave" Sortable="true"></ext:Column>
                                    <ext:Column DataIndex="PLANTILLAS_NOMBRE"  Header="Nombre" Sortable="true" ></ext:Column>
                                    <ext:Column DataIndex="PLANTILLAS_ASUNTO"  Header="Asunto" Sortable="true"></ext:Column>

                                    <ext:Column DataIndex="PLANTILLAS_LLAVE" Width="28" Sortable="false" MenuDisabled="true" Header="&nbsp;" Fixed="true">
                                        <Renderer Handler="return '';" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="EditarBtn" runat="server" Text="Editar" Icon="EmailEdit">
                                            <Listeners>
                                                <Click Handler="PageX.edit();" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="AuditoriaBtn" runat="server" Text="Auditoria" Icon="Cog">
                                            <Listeners>
                                                <Click Handler="PageX.showAudit();" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <HeaderRows>
                                        <ext:HeaderRow>
                                            <Columns>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PLANTILLAS_LLAVE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="25">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PLANTILLAS_NOMBRE" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="45">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>
                                                <ext:HeaderColumn Cls="x-small-editor">
                                                    <Component>
                                                        <ext:TextField ID="f_PLANTILLAS_ASUNTO" runat="server" EnableKeyEvents="true" Icon="Find" MaxLength="100">
                                                            <Listeners>
                                                                <KeyUp Handler="PageX.keyUpEvent(this, e);" />
                                                            </Listeners>
                                                        </ext:TextField>
                                                    </Component>
                                                </ext:HeaderColumn>

                                                <ext:HeaderColumn AutoWidthElement="false">
                                                    <Component>
                                                        <ext:Button ID="ClearFilterButton" runat="server" Icon="Cancel">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="Clear filter" />
                                                            </ToolTips>
                                                            <Listeners>
                                                                <Click Handler="PageX.clearFilter();" />
                                                            </Listeners>                                            
                                                        </ext:Button>
                                                    </Component>
                                                </ext:HeaderColumn>
                                            </Columns>
                                        </ext:HeaderRow>
                                    </HeaderRows>
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" PageSize="20" StoreID="PlantillasSt" />
                            </BottomBar>
                            <LoadMask ShowMask="true" />
                            <SaveMask ShowMask="true" />
                            <Listeners>
                                <RowDblClick Handler="PageX.edit();" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Store ID="FormatKeysSt" runat="server" SkipIdForNewRecords="false" AutoLoad="false" OnRefreshData="FormatKeysSt_Refresh">
            <Reader>
                <ext:JsonReader IDProperty="Value">
                    <Fields>
                        <ext:RecordField Name="Value" />
                        <ext:RecordField Name="Text" />
                    </Fields>
                </ext:JsonReader>
            </Reader>
        </ext:Store>

        <ext:Window ID="EditarPlantillasWin"
            runat="server"
            Hidden="true"
            Icon="EmailEdit"
            Title="Editar Plantilla de Notificación"
            Width="600"
            AutoHeight="true"
            Resizable="false"
            Shadow="None"
            Modal="true"
            X="10" Y="30">
            <Listeners>
                <Show Handler="#{EditMensajeTxt}.show();" />
            </Listeners>
            <Items>
                <ext:FormPanel ID="EditarPlantillaFormP" runat="server" Title="Form Panel" Header="false" ButtonAlign="Right" MonitorValid="true" Layout="FormLayout">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Title="Plantilla" Layout="FormLayout" Padding="5" Resizable="false">
                            <Items>
                                <ext:TextField runat="server" ID="EditLlaveTxt"            DataIndex="PLANTILLAS_LLAVE"  LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Llave de Plantilla" AllowBlank="false" ReadOnly="true">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Title="LLave de Plantilla" Html="La llave de Plantilla es de solo lectura." Width="200" TrackMouse="true" />
                                    </ToolTips>
                                </ext:TextField>
                                <ext:TextField runat="server"   ID="EditNombreTxt"        DataIndex="PLANTILLAS_NOMBRE"  LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Nombre" AllowBlank="false" MsgTarget="Side" MaxLength="45" ></ext:TextField>
                                <ext:TextField runat="server"   ID="EditAsuntoTxt"        DataIndex="PLANTILLAS_ASUNTO"  LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Asunto" MaxLength="100"></ext:TextField>
                                <ext:ComboBox runat="server"    ID="EditInsertFormatKey"                                 LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Agregar Detalles"
                                    StoreID="FormatKeysSt" 
                                    EmptyText="Seleccione un Detalle"
                                    ValueField="Value" 
                                    DisplayField="Text" 
                                    Mode="Local"
                                    TypeAhead="true">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="SimpleAdd" Qtip="Agregar Detalle" />
                                    </Triggers>
                                    <Listeners>
                                        <BeforeQuery Handler="this.triggers[0][ this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                        <TriggerClick Handler="if (index == 0) { this.focus().clearValue(); trigger.hide(); } else { #{EditMensajeTxt}.insertAtCursor(#{EditInsertFormatKey}.getText()); }" />
                                        <Select Handler="this.triggers[0].show();" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:Container ID="Container1" runat="server" Layout="Form" >
                                    <Items>
                                        <ext:HtmlEditor runat="server" ID="EditMensajeTxt" DataIndex="PLANTILLAS_MENSAJE" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Mensaje" AllowBlank="false" MsgTarget="Side" MaxLength="45" HideLabel="true" Height="300" Hidden="true" />
                                    </Items>
                                </ext:Container>
                                <ext:TextField runat="server"   ID="EditCreatedByTxt"     DataIndex="CREADO_POR"         LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Creado_por" Hidden="true" ></ext:TextField>
                                <ext:TextField runat="server"   ID="EditCreationDateTxt"  DataIndex="FECHA_CREACION"     LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha de Creacion" Hidden="true" ></ext:TextField>
                                <ext:TextField runat="server"   ID="EditModifiedByTxt"    DataIndex="MODIFICADO_POR"     LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Modificado por" Hidden="true" ></ext:TextField>
                                <ext:TextField runat="server"   ID="EditModificationDate" DataIndex="FECHA_MODIFICACION" LabelAlign="Right" AnchorHorizontal="100%" FieldLabel="Fecha de Modificacion" Hidden="true" ></ext:TextField>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="EditPreviousBtn" runat="server" Text="Anterior" Icon="PreviousGreen">
                            <Listeners>
                                <Click Handler="PageX.previous();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditNextBtn" runat="server" Text="Siguiente" Icon="NextGreen">
                            <Listeners>
                                <Click Handler="PageX.next();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="EditGuardarBtn" runat="server" Text="Guardar" Icon="Disk" FormBind="true">
                            <Listeners>
                                <Click Handler="#{EditModifiedByTxt}.setValue(#{LoggedUserHdn}.getValue()); PageX.update();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>