import { Button, Table } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { useCoursePermission } from '../../auth';
import { AuthItemColumn } from '../../common/types';
import { TableContainer } from '../../components';
import { TestAttemptItem } from '../../models/test.interface';
import { DATE_TIME_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  courseId: string | undefined;
  attemps: TestAttemptItem[];
}

export default function ResultsPreviousAttemptsTable({
  courseId,
  attemps,
}: Props) {
  const { t } = useTranslation();

  const { filterColumns } = useCoursePermission(courseId);

  const columns: AuthItemColumn<TestAttemptItem>[] = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'startedAt',
      title: t('course_test.started_at'),
      render: (_, record) => dayjs(record.startedAt).format(DATE_TIME_FORMAT),
    },
    {
      key: 'finishedAt',
      title: t('course_test.finished_at'),
      render: (_, record) =>
        record.finishedAt && dayjs(record.finishedAt).format(DATE_TIME_FORMAT),
    },
    {
      key: 'grade',
      dataIndex: 'grade',
      title: t('course_test.grade'),
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) =>
        !record.isCompleted && (
          <Link
            to={`/courses/${courseId}/test/attempt/${record.testSessionId}`}
          >
            <Button className="btn-success" type="primary">
              {t('course_test.continue_attempt')}
            </Button>
          </Link>
        ),
      show: attemps.some((item) => !item.isCompleted),
    },
  ];

  return (
    <TableContainer className="mb-field">
      <Table
        bordered
        columns={filterColumns(columns)}
        dataSource={attemps}
        rowKey={(record) => record.testSessionId}
        pagination={false}
      />
    </TableContainer>
  );
}
