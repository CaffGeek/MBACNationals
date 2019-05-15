<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_contingents.jpg');"></div>
    <div id="photoCredit"><strong>Casino du Lac-Leamy Fireworks</strong> &bull; Credit: fw42 - Flickr Commons</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="row" data-ng-controller="ContingentController">
        <div class="col-md-2 hidden-print">
            <ul class="sidebarNav">
                <li><a ui-sref="contingents({province: 'BC'})">British Columbia</a></li>
                <li><a ui-sref="contingents({province: 'AB'})">Alberta</a></li>
                <li><a ui-sref="contingents({province: 'SK'})">Saskatchewan</a></li>
                <li><a ui-sref="contingents({province: 'MB'})">Manitoba</a></li>
                <li><a ui-sref="contingents({province: 'NO'})">Northern Ontario</a></li>
                <li><a ui-sref="contingents({province: 'SO'})">Southern Ontario</a></li>
                <li><a ui-sref="contingents({province: 'QC'})">Quebec</a></li>
                <li><a ui-sref="contingents({province: 'NL'})">Newfoundland &amp; Labrador</a></li>
            </ul>
        </div>

        <div class="col-md-10" ui-view=""></div>
    </div>
</asp:Content>
