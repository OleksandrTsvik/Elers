import { CalendarOutlined } from '@ant-design/icons';
import { List } from 'antd';
import dayjs from 'dayjs';

import { SubmittedAssignmentListItemResponse } from '../../api/assignments.api';
import { IconText } from '../../components';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  item: SubmittedAssignmentListItemResponse;
}

export default function SubmittedAssignmentsListItem({
  item: {
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
        title={assignmentTitle}
        description={`${studentLastName} ${studentFirstName} ${studentPatronymic}`}
      />
    </List.Item>
  );
}
