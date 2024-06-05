import { Col, Row } from 'antd';
import { useState } from 'react';

import SubmittedAssignmentsHead from './submitted-assignments.head';
import SubmittedAssignmentsList from './submitted-assignments.list';
import SubmittedAssignmentsSelectAssignment from './submitted-assignments.select-assignment';
import SubmittedAssignmentsSelectStatus from './submitted-assignments.select-status';
import SubmittedAssignmentsSelectStudent from './submitted-assignments.select-student';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

export default function SubmittedAssignmentsPage() {
  const [status, setStatus] = useState(SubmittedAssignmentStatus.Submitted);
  const [assignmentId, setAssignmentId] = useState<string>();
  const [studentId, setStudentId] = useState<string>();

  return (
    <>
      <SubmittedAssignmentsHead />

      <Row gutter={[16, 16]}>
        <Col xs={24} sm={12}>
          <SubmittedAssignmentsSelectStatus
            status={status}
            onChange={setStatus}
          />
        </Col>

        <Col xs={24} sm={12}>
          <SubmittedAssignmentsSelectStudent
            value={studentId}
            onChange={setStudentId}
          />
        </Col>

        <Col xs={24}>
          <SubmittedAssignmentsSelectAssignment
            value={assignmentId}
            onChange={setAssignmentId}
          />
        </Col>
      </Row>

      <SubmittedAssignmentsList
        status={status}
        assignmentId={assignmentId}
        studentId={studentId}
      />
    </>
  );
}
