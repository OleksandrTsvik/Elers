import { TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import useUsersActions from './use-users.actions';
import { useGetListUserRolesQuery } from '../../api/roles.api';
import { ActionsDropdown } from '../../components';
import { GetColumnSearchProps } from '../../hooks/use-table-search-props';
import { User, UserType } from '../../models/user.interface';

export default function useUsersColumns(
  getColumnSearchProps: GetColumnSearchProps<User>,
) {
  const { t } = useTranslation();

  const { data: roles } = useGetListUserRolesQuery();

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
      sorter: true,
      ...getColumnSearchProps('lastName', t('users_page.lastName')),
    },
    {
      key: 'firstName',
      dataIndex: 'firstName',
      title: t('users_page.firstName'),
      sorter: true,
      ...getColumnSearchProps('firstName', t('users_page.firstName')),
    },
    {
      key: 'patronymic',
      dataIndex: 'patronymic',
      title: t('users_page.patronymic'),
      sorter: true,
      ...getColumnSearchProps('patronymic', t('users_page.patronymic')),
    },
    {
      key: 'email',
      dataIndex: 'email',
      title: t('users_page.email'),
      sorter: true,
      ...getColumnSearchProps('email', t('users_page.email')),
    },
    {
      key: 'roles',
      title: t('users_page.roles'),
      render: (_, record) => record.roles.join(', '),
      filters: roles?.map((item) => ({ text: item.name, value: item.id })),
    },
    {
      key: 'type',
      title: t('users_page.user_type'),
      render: (_, { type }) => t([`users_page.user_types.${type}`, type]),
      filters: Object.values(UserType).map((type) => ({
        text: t([`users_page.user_types.${type}`, type]),
        value: type,
      })),
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) => <ActionsDropdown items={getActionItems(record)} />,
    },
  ];

  return columns;
}
