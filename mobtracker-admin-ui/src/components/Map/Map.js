import React, { useState } from "react";
import MapGL, {
  Marker,
  Popup,
  NavigationControl,
  FullscreenControl
} from "react-map-gl";
import DevicePin from "components/Map/DevicePin";

export default function Map({ locationMarkers }) {
  const [viewport, setViewport] = useState({
    latitude: 12.345,
    longitude: 12.345,
    zoom: 8
  });

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
        <DevicePin size={20} color={locationMarker.colour} />
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
      <FullscreenControl />
      <NavigationControl />
    </MapGL>
  );
}
