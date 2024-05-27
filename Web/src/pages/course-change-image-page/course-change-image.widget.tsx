import { Button } from 'antd';
import { RcFile } from 'antd/es/upload';
import { useEffect, useState } from 'react';
import { Cropper } from 'react-cropper';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { useChangeCourseImageMutation } from '../../api/courses.api';
import { ErrorAlert } from '../../common/error';
import { FileDropzone } from '../../common/file-upload';
import {
  IMAGE_SIZE_LIMIT,
  IMAGE_SIZE_LIMIT_MB,
} from '../../utils/constants/app.constants';

import styles from './course-change-image.module.scss';

interface Props {
  courseId: string;
}

export default function CourseChangeImageWidget({ courseId }: Props) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [cropper, setCropper] = useState<Cropper>();
  const [previewImg, setPreviewImg] = useState<string>();

  const [errorMessage, setErrorMessage] = useState<string>();
  const [filename, setFilename] = useState<string>();

  const [changeCourseImage, { isLoading, error }] =
    useChangeCourseImageMutation();

  useEffect(() => {
    return () => {
      if (previewImg) {
        URL.revokeObjectURL(previewImg);
      }
    };
  }, [previewImg]);

  const handleChangeImage = (file: RcFile): boolean => {
    if (file.size >= IMAGE_SIZE_LIMIT) {
      setErrorMessage(t('validation_rule.file_size_limit'));
      return false;
    }

    if (previewImg) {
      URL.revokeObjectURL(previewImg);
    }

    setErrorMessage(undefined);
    setFilename(file.name);
    setPreviewImg(URL.createObjectURL(file));

    return false;
  };

  const handleSubmit = () => {
    const croppedCanvas = cropper?.getCroppedCanvas();

    if (!croppedCanvas) {
      return;
    }

    croppedCanvas.toBlob(async (blob) => {
      if (!blob) {
        return;
      }

      await changeCourseImage({
        id: courseId,
        image: blob,
        filename,
      })
        .unwrap()
        .then(() => navigate('/courses'));
    });
  };

  return (
    <>
      <ErrorAlert className={styles.errorAlert} error={errorMessage || error} />
      <FileDropzone
        showUploadList={false}
        accept="image/*"
        fileSizeLimitMb={IMAGE_SIZE_LIMIT_MB}
        beforeUpload={handleChangeImage}
      />
      <Cropper
        className={styles.cropper}
        background={false}
        dragMode="move"
        viewMode={1}
        initialAspectRatio={3 / 2}
        src={previewImg}
        onInitialized={(cropper) => setCropper(cropper)}
      />
      <Button
        block
        type="primary"
        disabled={!previewImg}
        loading={isLoading}
        onClick={handleSubmit}
      >
        {t('actions.change_image')}
      </Button>
    </>
  );
}
