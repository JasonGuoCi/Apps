<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="swfupload_demo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
<script type="text/javascript">
    //FLVupload.aspx参数
    //CompletedFunction:上传成功后调用的客户端JS事件
    //FileExtension:允许上传的文件类型
    //filesize:允许上传的文件大小
                    document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0\"');
                    document.write('width=\"550\" height=\"45\" id=\"Object1\" align=\"middle\">')
                    document.write('<param name=\"allowScriptAccess\" value=\"sameDomain\" />');
                    document.write('<param name=\"movie\" value=\"SWFFileUpload.swf\" />');
                    document.write('<param name=\"quality\" value=\"high\" />');
                    document.write('<param name=\"wmode\" value=\"transparent\">');
                    document.write('<param name=\"FlashVars\" value=\"UploadPage=FLVUpload.aspx&Args=ParentPath=&CompletedFunction=OnCompleted&FileExtension=*.mpeg;*.avi;*.mp4;*.3gp;*.rm;*.rmvb;*.mov;*.wmv;*.flv;*.asf&filesize=204800\">');
                    document.write('<embed src=\"SWFFileUpload.swf\" name=\"fileUpload2\" align=\"middle\"');
                    document.write('allowscriptaccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');
                    document.write('</object>');
                    function OnCompleted() {
                        alert("上传成功!");
                    }
                </script>
</body>
</html>
