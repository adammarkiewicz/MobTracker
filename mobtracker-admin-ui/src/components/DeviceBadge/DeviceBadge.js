import React from "react";
import { Row, Col, ListGroup } from "react-bootstrap";
import phone from "assets/phone.svg";

export default function DeviceBadge({ device }) {
  return (
    <Row>
      <Col>
        <ListGroup.Item
          action
          className="d-flex justify-content-between align-items-center"
        >
          <div>
            <img src={phone} alt="device" />
          </div>
          <div className="flex-column">
            {device.id}
            <p>
              <small>{device.connectionId}</small>
            </p>
            <p>
              <small>{device.manufacturer}</small>
            </p>
            <span className="badge badge-info badge-pill">{device.model}</span>
          </div>
        </ListGroup.Item>
      </Col>
    </Row>
  );
}
