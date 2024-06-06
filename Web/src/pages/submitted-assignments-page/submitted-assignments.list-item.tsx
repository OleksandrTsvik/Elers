import { CalendarOutlined } from '@ant-design/icons';
import { List } from 'antd';
import dayjs from 'dayjs';
import { Link } from 'react-router-dom';

import { SubmittedAssignmentListItemResponse } from '../../api/assignments.api';
import { IconText } from '../../components';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  courseId: string | undefined;
  item: SubmittedAssignmentListItemResponse;
}

export default function SubmittedAssignmentsListItem({
  courseId,
  item: {
    submittedAssignmentId,
    assignmentTitle,
    studentLastName,
    studentFirstName,
    studentPatronymic,
    submittedDate,
  },
}: Props) {
  return (
    <List.Item
      actions={[
        <IconText
          icon={CalendarOutlined}
          text={dayjs(submittedDate).format(DATE_FORMAT)}
        />,
      ]}
    >
      <List.Item.Meta
        title={
          <Link
            to={`/courses/${courseId}/submitted-assignments/${submittedAssignmentId}`}
          >
            {assignmentTitle}
          </Link>
        }
        description={`${studentLastName} ${studentFirstName} ${studentPatronymic}`}
      />
    </List.Item>
  );
}
