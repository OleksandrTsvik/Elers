import { List } from 'antd';
import { useParams } from 'react-router-dom';

import SubmittedAssignmentsListItem from './submitted-assignments.list-item';
import { useGetListSubmittedAssignmentsQuery } from '../../api/assignments.api';
import usePagination from '../../hooks/use-pagination';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

interface Props {
  status: SubmittedAssignmentStatus;
  assignmentId?: string;
  studentId?: string;
}

export default function SubmittedAssignmentsList({
  status,
  assignmentId,
  studentId,
}: Props) {
  const { courseId } = useParams();

  const { pagination, pagingParams } = usePagination();

  const { data, isFetching } = useGetListSubmittedAssignmentsQuery({
    courseId,
    ...pagingParams,
    status,
    assignmentId,
    studentId,
  });

  return (
    <List
      className="mt-field"
      size="large"
      itemLayout="vertical"
      loading={isFetching}
      dataSource={data?.items}
      pagination={pagination(data?.pageSize, data?.totalCount)}
      rowKey={(item) => item.submittedAssignmentId}
      renderItem={(item) => <SubmittedAssignmentsListItem item={item} />}
    />
  );
}
