namespace Blazor.Mapy.cz;

public static class MapySetup
{
    public static string RenderLeafletSetup()
    {
        return """
        <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.2/dist/leaflet.css" integrity="sha256-sA+zWATbFveLLNqWO2gtiw3HL/lh1giY/Inf1BJ0z14=" crossorigin="" />
        <script src="https://unpkg.com/leaflet@1.9.2/dist/leaflet.js" integrity="sha256-o9N1jGDZrf5tS+Ft4gbIK7mYMipq9lqpVJ91xHSyKhg=" crossorigin=""></script>
        """;
    }

    public static string RenderMaplibre()
    {
        return """
               <script src='https://unpkg.com/maplibre-gl@latest/dist/maplibre-gl.js'></script>
               <link href='https://unpkg.com/maplibre-gl@latest/dist/maplibre-gl.css' rel='stylesheet' />
               """;
    }
}