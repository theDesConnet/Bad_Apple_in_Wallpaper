@echo off
mkdir Frames
ffmpeg.exe -i BA_Original.mp4 Frames/frame_%02d.bmp
