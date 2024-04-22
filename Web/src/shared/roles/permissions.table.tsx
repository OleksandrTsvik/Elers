import { Table } from 'antd';
import { Key } from 'antd/es/table/interface';

import usePermissionsColumns from './use-permissions.columns';
import { TableContainer } from '../../components';
import { Permission } from '../../models/permission.interface';

interface Props {
  permissions: Permission[];
  defaultSelectedRowKeys?: Key[];
  onChangeRowSelection: (selectedRowKeys: Key[]) => void;
}

export default function PermissionsTable({
  permissions,
  defaultSelectedRowKeys,
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
          defaultSelectedRowKeys,
          onChange: onChangeRowSelection,
        }}
        columns={columns}
        dataSource={permissions}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
