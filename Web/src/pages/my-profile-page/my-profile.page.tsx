import { EditOutlined, LockOutlined } from '@ant-design/icons';
import { Button, Flex, Skeleton, Space } from 'antd';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';

import MyProfileAvatar from './my-profile.avatar';
import MyProfileData from './my-profile.data';
import MyProfileForm from './my-profile.form';
import MyProfileHead from './my-profile.head';
import MyProfilePasswordForm from './my-profile.password-form';
import { useGetCurrentProfileQuery } from '../../api/profile.api';
import { PermissionType, PermissionsGuard } from '../../auth';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MyProfilePage() {
  const { t } = useTranslation();

  const [editing, setEditing] = useState(false);
  const [changingPassword, setChangingPassword] = useState(false);

  const { data, isFetching, error } = useGetCurrentProfileQuery();

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MyProfileHead title={`${data.lastName} ${data.firstName}`} />

      <Flex vertical align="center" gap="small">
        <MyProfileAvatar avatarUrl={data.avatarUrl} />

        {!editing && !changingPassword && (
          <>
            <MyProfileData profile={data} />

            <Space>
              <PermissionsGuard permissions={PermissionType.UpdateOwnProfile}>
                <Button
                  icon={<EditOutlined />}
                  onClick={() => setEditing(true)}
                >
                  {t('my_profile_page.edit_data')}
                </Button>
              </PermissionsGuard>

              <PermissionsGuard permissions={PermissionType.UpdateOwnPassword}>
                <Button
                  danger
                  type="dashed"
                  icon={<LockOutlined />}
                  onClick={() => setChangingPassword(true)}
                >
                  {t('my_profile_page.change_password')}
                </Button>
              </PermissionsGuard>
            </Space>
          </>
        )}

        {editing && (
          <MyProfileForm
            initialValues={data}
            onFinish={() => setEditing(false)}
          />
        )}

        {changingPassword && (
          <MyProfilePasswordForm onFinish={() => setChangingPassword(false)} />
        )}
      </Flex>
    </>
  );
}
