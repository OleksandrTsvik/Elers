import { TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import useUsersActions from './use-users.actions';
import { ActionsDropdown } from '../../components';
import { User } from '../../models/user.interface';

export default function useUsersColumns() {
  const { t } = useTranslation();

  const { getActionItems } = useUsersActions();

  const columns: TableColumnsType<User> = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'lastName',
      dataIndex: 'lastName',
      title: t('users_page.lastName'),
    },
    {
      key: 'firstName',
      dataIndex: 'firstName',
      title: t('users_page.firstName'),
    },
    {
      key: 'patronymic',
      dataIndex: 'patronymic',
      title: t('users_page.patronymic'),
    },
    {
      key: 'email',
      dataIndex: 'email',
      title: t('users_page.email'),
    },
    {
      key: 'roles',
      title: t('users_page.roles'),
      render: (_, record) => record.roles.join(', '),
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) => <ActionsDropdown items={getActionItems(record)} />,
    },
  ];

  return columns;
}
