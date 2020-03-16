import React from "react";
import DeviceList from "components/DeviceList/DeviceList";
import { Row, Col } from "react-bootstrap";
import { useApi } from "contexts/Api";

export default function Location() {
  const { isLoading, trigerIntroductionOfConnectedDevices } = useApi();
  if (!isLoading) {
    trigerIntroductionOfConnectedDevices();
  }

  const list = [
    { id: 1, brand: "Samsung", model: "Galaxy S" },
    { id: 2, brand: "IPhone", model: "X" }
  ];
  return (
    <Row noGutters>
      <Col xs={3}>
        <DeviceList list={list} />
      </Col>
      <Col xs={9}>Map</Col>
    </Row>
  );
}
