import { CloseCircleOutlined, UploadOutlined } from '@ant-design/icons';
import { Button, Flex, Upload } from 'antd';
import { RcFile } from 'antd/es/upload';
import { useTranslation } from 'react-i18next';

import {
  useChangeAvatarMutation,
  useDeleteAvatarMutation,
} from '../../api/profile.api';
import { changeAvatarUrl } from '../../auth';
import { UserAvatar } from '../../components';
import { useAppDispatch } from '../../hooks/redux-hooks';
import useDisplayError from '../../hooks/use-display-error';
import { IMAGE_SIZE_LIMIT } from '../../utils/constants/app.constants';

interface Props {
  avatarUrl: string | undefined;
}

export default function MyProfileAvatar({ avatarUrl }: Props) {
  const { t } = useTranslation();

  const appDispatch = useAppDispatch();
  const { displayError } = useDisplayError();

  const [changeAvatar, { isLoading }] = useChangeAvatarMutation();

  const [deleteAvatar, { isLoading: isLoadingDelete }] =
    useDeleteAvatarMutation();

  const handleChangeImage = async (file: RcFile): Promise<boolean> => {
    if (file.size >= IMAGE_SIZE_LIMIT) {
      displayError(t('validation_rule.file_size_limit'));
      return false;
    }

    await changeAvatar({ avatar: file, filename: file.name })
      .unwrap()
      .then((avatarUrl) => appDispatch(changeAvatarUrl(avatarUrl)))
      .catch((error) => displayError(error));

    return false;
  };

  const handleDelete = async () => {
    await deleteAvatar()
      .unwrap()
      .then(() => appDispatch(changeAvatarUrl(undefined)))
      .catch((error) => displayError(error));
  };

  return (
    <>
      <UserAvatar url={avatarUrl} size={120} />

      <Flex wrap justify="center" gap="middle">
        <Upload
          maxCount={1}
          showUploadList={false}
          accept="image/*"
          beforeUpload={handleChangeImage}
        >
          <Button type="dashed" icon={<UploadOutlined />} loading={isLoading}>
            {t('my_profile_page.change_avatar')}
          </Button>
        </Upload>
        <Button
          icon={<CloseCircleOutlined />}
          loading={isLoadingDelete}
          onClick={handleDelete}
        >
          {t('actions.delete')}
        </Button>
      </Flex>
    </>
  );
}
