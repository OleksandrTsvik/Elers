import { Table, TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import { useGetListCoursePermissionsQuery } from '../../api/course-permissions.api';
import { TableContainer } from '../../components';
import { CoursePermissionListItem } from '../../models/course-permission.interface';

export default function CoursePermissionsTab() {
  const { t } = useTranslation();
  const { data, isFetching } = useGetListCoursePermissionsQuery();

  const columns: TableColumnsType<CoursePermissionListItem> = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: t('permissions_page.description'),
    },
  ];

  return (
    <TableContainer>
      <Table
        loading={isFetching}
        columns={columns}
        dataSource={data}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
