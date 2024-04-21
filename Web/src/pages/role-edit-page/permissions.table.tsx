import { Table } from 'antd';
import { Key } from 'antd/es/table/interface';

import { getDefaultSelectedRowKeys } from './role-edit-page.utils';
import usePermissionsColumns from './use-permissions.columns';
import { TableContainer } from '../../components';
import { RolePermissions } from '../../models/permission.interface';

interface Props {
  data: RolePermissions[];
  onChangeRowSelection: (selectedRowKeys: Key[]) => void;
}

export default function PermissionsTable({
  data,
  onChangeRowSelection,
}: Props) {
  const columns = usePermissionsColumns();

  return (
    <TableContainer>
      <Table
        bordered
        size="small"
        rowSelection={{
          type: 'checkbox',
          defaultSelectedRowKeys: getDefaultSelectedRowKeys(data),
          onChange: onChangeRowSelection,
        }}
        columns={columns}
        dataSource={data}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
