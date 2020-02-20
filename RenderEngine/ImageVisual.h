#pragma once

#include "IVisual.h"

#include <vtkImageSliceMapper.h>
#include <vtkImageSlice.h>

class ImageVisualPrivates;

namespace RenderEngine
{
    public ref class ImageVisual : public IVisual
    {
    public:
        ImageVisual(System::Collections::Generic::List<ImageData^>^ images);
        !ImageVisual();
        ~ImageVisual();

        virtual void AddTo(ViewportRenderer ^ viewport);
        virtual void RemoveFrom(ViewportRenderer ^ viewport);

        double GetWindowLevel();
        double GetWindowWidth();
        void SetWindowing(double level, double width);

        int GetImageIndex();
        int GetNumberOfImages();
        void SetImageIndex(int index);

    private:
        ImageVisualPrivates* privates;

        System::Collections::Generic::List<ImageData^>^ images;
        int numberOfImages;
        int currentImageIndex;
    };
}
