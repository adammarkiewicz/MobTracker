import React from "react";
import DeviceBadge from "components/DeviceBadge/DeviceBadge";
import ListGroup from "react-bootstrap/ListGroup";

export default function DeviceList({ list, onBadgeClick }) {
  return (
    <ListGroup>
      {list.map(device => (
        <DeviceBadge
          key={device.id}
          device={device}
          onBadgeClick={onBadgeClick}
        />
      ))}
    </ListGroup>
  );
}
