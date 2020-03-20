import React from "react";
import MapGL, {
  Marker,
  Popup,
  NavigationControl,
  FullscreenControl
} from "react-map-gl";
import DevicePin from "components/Map/DevicePin";
import "components/Map/Map.css";

export default function Map({ viewport, setViewport, locationMarkers }) {
  const config = {
    accessToken: process.env.REACT_APP_MAPBOX_TOKEN
  };

  const renderDeviceMarker = locationMarker => {
    return (
      <Marker
        key={`marker-${locationMarker.deviceId}`}
        longitude={locationMarker.longitude}
        latitude={locationMarker.latitude}
      >
        <DevicePin size={30} color={locationMarker.colour} />
      </Marker>
    );
  };

  return (
    <MapGL
      {...viewport}
      width="100%"
      height="100%"
      mapboxApiAccessToken={config.accessToken}
      onViewportChange={setViewport}
    >
      {locationMarkers.map(marker => renderDeviceMarker(marker))}

      <div className="FullscreenControl">
        <FullscreenControl />
      </div>
      <div className="NavigationControl">
        <NavigationControl />
      </div>
    </MapGL>
  );
}
