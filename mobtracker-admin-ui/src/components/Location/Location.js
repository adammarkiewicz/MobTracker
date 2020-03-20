import React, { useState, useEffect } from "react";
import DeviceList from "components/DeviceList/DeviceList";
import { Row, Col } from "react-bootstrap";
import { useApi } from "contexts/Api";
import Map from "components/Map/Map";

export default function Location() {
  const {
    isLoading,
    connection,
    trigerDevicesIntroduction,
    startTrackingAllDevices,
    stopTrackingAllDevices,
    onNewDevice,
    offNewDevice
  } = useApi();

  const [deviceList, setDeviceList] = useState([
    {
      id: 1,
      connectionId: 1,
      manufacturer: "Samsung",
      model: "Galaxy S",
      colour: "blue"
    }
  ]);

  const [locationMarkers, setLocationMarkers] = useState([
    { deviceId: 1, colour: "blue", latitude: 12.345, longitude: 12.345 }
  ]);

  useEffect(() => {
    if (!isLoading) {
      onNewDevice(device => {
        setDeviceList(devices => [...devices, device]);
      });

      connection.on("DeviceDisconnected", connectionId => {
        setDeviceList(devices =>
          devices.filter(device => device.connectionId !== connectionId)
        );
      });

      connection.on("NewLocationMarker", locationMarker => {
        console.log("NewLocationMarker", locationMarker);
        setLocationMarkers(locationMarkers => {
          let markerIndex = locationMarkers.findIndex(
            marker => marker.deviceId === locationMarker.deviceId
          );
          if (markerIndex === -1) {
            return [...locationMarkers, locationMarker];
          } else {
            let newMarkers = [...locationMarkers];
            newMarkers[markerIndex] = locationMarker;
            return newMarkers;
          }
        });
      });

      trigerDevicesIntroduction();
      startTrackingAllDevices();

      return function cleanup() {
        stopTrackingAllDevices();

        offNewDevice();
        connection.off("DeviceDisconnected");
        connection.off("NewLocationMarker");
      };
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isLoading]);

  return (
    <Row noGutters className="h-100">
      <Col xs={3}>
        <DeviceList list={deviceList} />
      </Col>
      <Col xs={9}>
        <Map locationMarkers={locationMarkers}></Map>
      </Col>
    </Row>
  );
}
