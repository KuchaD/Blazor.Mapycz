// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
  return prompt(message, 'Type anything here');
}

export function SetMap(lat, lng, zoom, firstRender, API_KEY){
  var map = new maplibregl.Map({
    container: 'map',
    center: [lng, lat], // starting position [lng, lat]
    zoom: zoom, // starting zoom
    style: {
      version: 8,
      sources: {
        // style for map tiles
        'basic-tiles': {
          type: 'raster',
          url: `https://api.mapy.cz/v1/maptiles/basic/tiles.json?apikey=${API_KEY}`,
          tileSize: 256,
        },
        // style for our geometry
        'route-geometry': {
          type: 'geojson',
          data: {
            type: "LineString",
            coordinates: [],
          },
        },
      },
      layers: [{
        id: 'tiles',
        type: 'raster',
        source: 'basic-tiles',
      }, {
        id: 'route-geometry',
        type: 'line',
        source: 'route-geometry',
        layout: {
          'line-join': 'round',
          'line-cap': 'round',
        },
        paint: {
          'line-color': '#0033ff',
          'line-width': 8,
          'line-opacity': 0.6,
        },
      }],
    },
  });

  class LogoControl {
    onAdd(map) {
      this._map = map;
      this._container = document.createElement('div');
      this._container.className = 'maplibregl-ctrl';
      this._container.innerHTML = '<a href="http://mapy.cz/" target="_blank"><img  width="100px" src="https://api.mapy.cz/img/api/logo.svg" ></a>';

      return this._container;
    }

    onRemove() {
      this._container.parentNode.removeChild(this._container);
      this._map = undefined;
    }
  }

  // we add our LogoControl to the map
  if (firstRender) {
    map.addControl(new LogoControl(), 'bottom-left');
  }
  
  return map;
}

export function SetMarker(map, lat, lng, color) {
  return new maplibregl.Marker({
    color: color,
  })
      .setLngLat([lng, lat])
      .addTo(map);
}

// function for calculating a bbox from an array of coordinates
function boundingBox(coords) {
  let minLatitude = Infinity;
  let minLongitude = Infinity;
  let maxLatitude = -Infinity;
  let maxLongitude = -Infinity;

  coords.forEach(coor => {
    minLongitude = Math.min(coor[0], minLongitude);
    maxLongitude = Math.max(coor[0], maxLongitude);
    minLatitude = Math.min(coor[1], minLatitude);
    maxLatitude = Math.max(coor[1], maxLatitude);
  });

  return [
    [minLongitude, minLatitude],
    [maxLongitude, maxLatitude],
  ];
}

export function RouteMap(map, geometry) {
  try {
    const source = map.getSource('route-geometry');

    if (source && geometry) {
      source.setData(geometry);
      map.jumpTo(map.cameraForBounds(boundingBox(geometry.geometry.coordinates), {
        padding: 40,
      }));
    }
  } catch (ex) {
    console.log(ex);
  }
  
  return map;
}
