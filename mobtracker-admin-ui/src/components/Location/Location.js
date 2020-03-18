import React, { useState, useEffect } from "react";
import DeviceList from "components/DeviceList/DeviceList";
import { Row, Col } from "react-bootstrap";
import { useApi } from "contexts/Api";

export default function Location() {
  const { isLoading, connection, trigerDevicesIntroduction } = useApi();

  const [deviceList, setDeviceList] = useState([
    { id: 1, connectionId: 1, manufacturer: "Samsung", model: "Galaxy S" },
    { id: 2, connectionId: 2, manufacturer: "IPhone", model: "X" }
  ]);

  useEffect(() => {
    if (!isLoading) {
      connection.on("NewDevice", device => {
        setDeviceList(devices => [...devices, device]);
      });

      connection.on("DeviceDisconnected", connectionId => {
        setDeviceList(devices =>
          devices.filter(device => device.connectionId !== connectionId)
        );
      });

      trigerDevicesIntroduction();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isLoading]);

  return (
    <Row noGutters>
      <Col xs={3}>
        <DeviceList list={deviceList} />
      </Col>
      <Col xs={9}>Map</Col>
    </Row>
  );
}
