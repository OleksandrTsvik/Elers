import PermissionsHead from './permissions.head';
import PermissionsTable from './permissions.table';
import PermissionsTitle from './permissions.title';

export default function PermissionsPage() {
  return (
    <>
      <PermissionsHead />
      <PermissionsTitle />
      <PermissionsTable />
    </>
  );
}
