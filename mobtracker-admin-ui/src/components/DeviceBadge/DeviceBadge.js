import React from "react";
import { Row, Col, ListGroup } from "react-bootstrap";
import phone from "assets/phone.svg";
import "components/DeviceBadge/DeviceBadge.css";

export default function DeviceBadge({ device, onBadgeClick }) {
  return (
    <Row>
      <Col>
        <ListGroup.Item
          action
          className="d-flex justify-content-between align-items-center"
          onClick={() => {
            console.log("Badge clicked! ", device.id);
            onBadgeClick(device.id);
          }}
        >
          <div>
            <img src={phone} alt="device" />
          </div>
          <div className="flex-column">
            {device.id}
            <div>
              <small>{device.connectionId}</small>
            </div>
            <div>
              <small>{device.manufacturer}</small>
            </div>
            <span
              style={{ backgroundColor: device.colour }}
              className="badge badge-info badge-pill"
            >
              {device.model}
            </span>
          </div>
        </ListGroup.Item>
      </Col>
    </Row>
  );
}
